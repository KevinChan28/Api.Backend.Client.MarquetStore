using Api.Client.MarquetStore.DTO;
using Api.Client.MarquetStore.Models;
using Api.Client.MarquetStore.Security;

namespace Api.Client.MarquetStore.Service
{
    public interface IUserService
    {
        Task<UserTokens> ValidateCredentials(Login login);
        Task<int> RegisterCustomer(UserRegister model);
        Task<List<User>> GetUsers();
        Task<bool> ValidateEmail(string email);
        Task<User> GetUserById(int idUser);
        Task<int> UpdateCustomer(UserUpdate model);
        Task<int> ChangePassword(ChangePassword model);
        Task SendCodeToRecoverPassword(string email);
        Task<bool> ValidateCodeToRecoverPassword(string code);
    }
}
