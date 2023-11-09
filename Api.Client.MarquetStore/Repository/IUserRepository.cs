using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.Repository
{
    public interface IUserRepository
    {
        Task<int> Add(User user);
        Task<bool> ValidateCredentials(User user);  
        Task<User> GetUserByCredentials(User user);
        Task<List<User>> GetUsers();
        Task<bool> ValidateEmail(string email);
        Task<User> GetUserById(int idUser);
        Task<int> Update(User user);
        Task<User> GetUserByEmail(string email);
    }
}
