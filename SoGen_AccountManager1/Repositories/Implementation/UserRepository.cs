
using Microsoft.EntityFrameworkCore;
using SoGen_AccountManager1.Data;
using SoGen_AccountManager1.Models.Domain;
using SoGen_AccountManager1.Repositories.Interface.IRepository;

namespace SoGen_AccountManager1.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> AddUser(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> FindUserById(int userId)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> FindUserByEmail(string email)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Mail == email);
        }

    }
}

