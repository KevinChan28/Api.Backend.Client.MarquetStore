using Api.Client.MarquetStore.DTO;
using Api.Client.MarquetStore.Models;
using Api.Client.MarquetStore.Repository;
using Api.Client.MarquetStore.Security;
using Api.Client.MarquetStore.Tools;

namespace Api.Client.MarquetStore.Service.Imp
{
    public class ImpUserService : IUserService
    {
        IUserRepository _userRepository;
        JwtSettings _jwtSettings;

        public ImpUserService(IUserRepository userRepository, JwtSettings jwtSettings)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<int> RegisterCustomer(UserRegister model)
        {
            User user = new User
            {
                Name = model.Name,
                LastName = model.LastName,
                Email = model.Email,
                Password = Encrypt.GetSHA256(model.Password),
                RolId = 2,
                Telephone = model.Telephone,
            };
            int userId = await _userRepository.Add(user);

            return userId;
        }

        public async Task<UserTokens> ValidateCredentials(Login login)
        {
            UserTokens token = new UserTokens();

            User user = new User
            {
                Email = login.Email,
                Password = Encrypt.GetSHA256(login.Password)
            };
            bool validate = await _userRepository.ValidateCredentials(user);

            if (validate)
            {
                User userValidate = await _userRepository.GetUserByCredentials(user);
                UserTokens informationUser = new UserTokens
                {
                    UserName = userValidate.Email,
                    EmailId = userValidate.Email,
                    Id = userValidate.Id,
                    Rol = userValidate.RolId,
                    GuidId = Guid.NewGuid(),
                };
                token = JwtHelpers.GenerateToken(informationUser, _jwtSettings);

                return token;
            }
            return null;
        }
    }
}
