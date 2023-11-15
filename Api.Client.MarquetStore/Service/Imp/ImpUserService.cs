using Api.Client.MarquetStore.DTO;
using Api.Client.MarquetStore.Models;
using Api.Client.MarquetStore.Repository;
using Api.Client.MarquetStore.Security;
using Api.Client.MarquetStore.Tools;
using Microsoft.Extensions.Caching.Memory;

namespace Api.Client.MarquetStore.Service.Imp
{
    public class ImpUserService : IUserService
    {
        IUserRepository _userRepository;
        JwtSettings _jwtSettings;
        ISend _sendEmail;
        private readonly IMemoryCache _memoryCache;
        IDatabaseRepository _databaseRepository;
        private readonly ILogger<ImpUserService> _logger;
        IViewRepository _viewRepository;

        public ImpUserService(IUserRepository userRepository, JwtSettings jwtSettings, ISend sendEmail
            , IMemoryCache memoryCache, IDatabaseRepository databaseRepository, ILogger<ImpUserService> logger, IViewRepository viewRepository)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings;
            _sendEmail = sendEmail;
            _memoryCache = memoryCache;
            _databaseRepository = databaseRepository;
            _logger = logger;
            _viewRepository = viewRepository;
        }

        public async Task<User> GetUserById(int idUser)
        {
            return await _userRepository.GetUserById(idUser);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<int> ChangePassword(ChangePassword model)
        {
            User user = await _userRepository.GetUserByEmail(model.Email);

            if (user != null)
            {
                user.Password = Encrypt.GetSHA256(model.Password);
                int idUser = await _userRepository.Update(user);

                return idUser;
            }

            return 0;
        }

        public async Task<int> RegisterCustomer(UserRegister model)
        {
            using(var transaction = await _databaseRepository.BeginTransaction())
            {
                try
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

                    if (userId > 0)
                    {
                        string htmlContent = await _viewRepository.GetHtmlWelcome();
                        EmailDTO emailDTO = new EmailDTO
                        {
                            Affair = "¡Bienvenido a Marquetstore!",
                            For = model.Email,
                            Content = htmlContent
                        };

                        bool success = await _sendEmail.SendEmail(emailDTO);

                        if (success)
                        {
                            transaction.Commit();
                            return userId;
                        }
                        else
                        {
                            transaction.Rollback();
                            return 0;
                        }
                    }
                }
                catch(Exception ex) 
                {
                    _logger.LogError(ex, "Error al enviar el correo.");
                    transaction.Rollback();
                }

                return 0;
            }
        }

        public async Task<int> UpdateCustomer(UserUpdate model)
        {
            User userFinded = await _userRepository.GetUserById(model.Id);

            if (userFinded != null)
            {
                userFinded.Email = model.Email;
                userFinded.LastName = model.LastName;
                userFinded.Telephone = model.Telephone;
                userFinded.Name = model.Name;
                int userId = await _userRepository.Update(userFinded);

                return userId;
            }

            return 0;
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
                    Id = userValidate.Id,
                    Rol = userValidate.RolId,
                    GuidId = Guid.NewGuid(),
                };
                token = JwtHelpers.GenerateToken(informationUser, _jwtSettings);

                return token;
            }
            return null;
        }

        public async Task<bool> ValidateEmail(string email)
        {
            bool exist = await _userRepository.ValidateEmail(email);

            if (exist)
            {
                await SendCodeToRecoverPassword(email);

                return true;
            }

            return false;
        }

        public async Task SendCodeToRecoverPassword(string email)
        {
            string code = CreateCode.GenerateUniqueCode();
            DateTimeOffset expiration = DateTimeOffset.Now.AddMinutes(10);

            _memoryCache.Set("code", code, expiration);

            string html = await _viewRepository.GetHtmñRecoverPassword();
            string htmlAddCode = html.Replace("{{code}}", $"{code}");

            EmailDTO emailDTO = new EmailDTO
            {
                    Affair = "Tu codigo para restablecer tu contraseña",
                    For = email,
                    Content = htmlAddCode
            };

            await _sendEmail.SendEmail(emailDTO);
        }

        public async Task<bool> ValidateCodeToRecoverPassword(string code)
        {
            if (_memoryCache.TryGetValue("code", out string storedCode) && code == storedCode)
            {
                _memoryCache.Remove("code");

                return true;
            }

            return false;
        }
    }
}
