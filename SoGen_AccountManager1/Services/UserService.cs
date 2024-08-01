using SoGen_AccountManager1.Models.Domain;
using SoGen_AccountManager1.Models.DTO;
using SoGen_AccountManager1.Repositories.Interface.IRepository;
using SoGen_AccountManager1.Repositories.Interface.IService;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;

namespace SoGen_AccountManager1.Services.UserService
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly JwtConfig _jwtConfig;

        public UserService(IUserRepository accountRepository, IOptions<JwtConfig> jwtConfig){
            _userRepository = accountRepository;
            _jwtConfig = jwtConfig.Value;
        }

        public async Task<(User User, string Token)> RegisterUserAsync(UserRegisterDTO userRegisterDTO)
        {
            // Validation des champs
            if (string.IsNullOrEmpty(userRegisterDTO.FirstName) || 
                string.IsNullOrEmpty(userRegisterDTO.LastName) || 
                string.IsNullOrEmpty(userRegisterDTO.Email) || 
                string.IsNullOrEmpty(userRegisterDTO.Password))
            {
                throw new ArgumentException("All fields are required.");
            }

            // Création de l'utilisateur
            var user = new User
            {
                FirstName = userRegisterDTO.FirstName,
                LastName = userRegisterDTO.LastName,
                Email = userRegisterDTO.Email,
                Password = HashPassword(userRegisterDTO.Password),
                Founded = DateTime.UtcNow
            };

            // Ajout de l'utilisateur dans la base de données
            await _userRepository.AddUserAsync(user);

            // Génération du JWT token
            var token = GenerateJwtToken(user);

            // Retourner l'utilisateur et le token
            return (user, token);
        }

        public async Task<(User User, string Token)> LoginAsync(string email, string password)
        {
            // Validation des champs
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Email and password are required.");
            }

            // Recherche de l'utilisateur par email
            var user = await _userRepository.FindUserByEmailAsync(email);

            // Vérification si l'utilisateur existe et si le mot de passe est correct
            if (user == null || !VerifyPassword(password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            // Génération du JWT token
            var token = GenerateJwtToken(user);

            // Retourner l'utilisateur et le token
            return (user, token);
        }


        public async Task<User> AddUserAsync(User user)
        {
            return await _userRepository.AddUserAsync(user);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _userRepository.FindUserByEmailAsync(email);
        }

        public async Task<bool> CheckIfUserExistsAsync(string email)
        {
            var user = await _userRepository.FindUserByEmailAsync(email);
            return user != null;
        }

        public async Task<bool> CheckPasswordAsync(string email, string password)
        {
            var user = await _userRepository.FindUserByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

        private bool VerifyPassword(string providedPassword, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(providedPassword, storedHash);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private string GenerateJwtToken(User user)
		{
			var jwtTokenHandler = new JwtSecurityTokenHandler();

			// Convertir la clé secrète en tableau d'octets
			var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

			// Créer la description du jeton
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim("Id", user.Id.ToString()), // Convertir l'ID en chaîne
					new Claim(JwtRegisteredClaimNames.Sub, user.Email),
					new Claim(JwtRegisteredClaimNames.Email, user.Email),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
				}),
				Expires = DateTime.UtcNow.AddHours(4), // Définir la durée de validité du jeton
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
															SecurityAlgorithms.HmacSha512) // Spécifier l'algorithme de signature
			};

			// Créer le jeton
			var token = jwtTokenHandler.CreateToken(tokenDescriptor);

			// Convertir le jeton en chaîne
			var jwtToken = jwtTokenHandler.WriteToken(token);

			return jwtToken;
		}

    }
}
