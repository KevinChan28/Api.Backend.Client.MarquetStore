using Api.Client.MarquetStore.Context;
using Api.Client.MarquetStore.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Api.Client.MarquetStore.Repository.Imp
{
    public class ImpUserRepository : IUserRepository
    {
        MarquetstoreDbContext _dbContext;

        public ImpUserRepository(MarquetstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<int> Add(User user)
        {
            EntityEntry<User> userNew = await _dbContext.Users.AddAsync(user);
              await _dbContext.SaveChangesAsync();

                return userNew.Entity.Id;
            
        }

        public async Task<User> GetUserByCredentials(User user)
        {
            return _dbContext.Users.FirstOrDefault(a => a.Email == user.Email && a.Password == user.Password);
        }

        public async Task<User> GetUserById(int idUser)
        {
            return _dbContext.Users.Where(i => i.Id == idUser).FirstOrDefault();
        }

        public async Task<List<User>> GetUsers()
        {
            return _dbContext.Users.AsEnumerable<User>().ToList();
        }

        public async Task<bool> ValidateCredentials(User user)
        {
            return _dbContext.Users.Any(a => a.Email == user.Email && a.Password == user.Password);
        }

        public async Task<bool> ValidateEmail(string email)
        {
            return _dbContext.Users.Any(x => x.Email.Equals(email));
        }
    }
}
