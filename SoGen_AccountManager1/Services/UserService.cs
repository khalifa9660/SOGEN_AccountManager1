using SoGen_AccountManager1.Models.Domain;
using SoGen_AccountManager1.Repositories.Interface.IRepository;
using SoGen_AccountManager1.Repositories.Interface.IService;

namespace SoGen_AccountManager1.Services.UserService
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository accountRepository){
            _userRepository = accountRepository;
        }

        public async Task<bool> CreateUserAsync(User user, string password)
		{
			// Hacher le mot de passe
			user.Password = HashPassword(password);

			// Ajouter l'utilisateur à la base de données
			await _userRepository.AddUser(user);

			return true;
		}

        		public string HashPassword(string password)
		{
			// Utiliser une bibliothèque de hachage sécurisée, comme BCrypt
			return BCrypt.Net.BCrypt.HashPassword(password);
		}

    }
}
