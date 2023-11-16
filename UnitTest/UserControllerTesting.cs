using Api.Client.MarquetStore.Controllers;
using Api.Client.MarquetStore.DTO;
using Api.Client.MarquetStore.Models;
using Api.Client.MarquetStore.Models.Others;
using Api.Client.MarquetStore.Repository;
using Api.Client.MarquetStore.Security;
using Api.Client.MarquetStore.Service;
using Api.Client.MarquetStore.Service.Imp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace UnitTest
{
    public class UserControllerTesting
    {
        private readonly UsersController _usersController;
        private readonly IUserService _userService;
        private readonly IUserRepository userRepository;
        private readonly JwtSettings _jwtSettings;
        private readonly ISend _sendEmail;
        private readonly IMemoryCache _memoryCache;
        private readonly IDatabaseRepository _databaseRepository;
        private readonly ILogger<ImpUserService> _logger;
        private readonly ILogger<UsersController> _loggerController;
        private readonly IViewRepository _viewRepository;

        public UserControllerTesting()
        {
            _userService = new ImpUserService(userRepository, _jwtSettings, _sendEmail,_memoryCache,_databaseRepository,_logger,_viewRepository);
            _usersController = new UsersController(_userService, _loggerController);
        }

        [Fact]
        public async Task GetUser_Ok()
        {
            // Arrange
            Mock<IUserService> mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.GetUsers()).ReturnsAsync(new List<User> { new User() }); // Usuarios ficticios para simular la respuesta exitosa
            UsersController usersController = new UsersController(mockUserService.Object, null);

            // Acttion
            var result = await usersController.GetAllUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            ResponseBase response = Assert.IsType<ResponseBase>(okResult.Value);
            Assert.True(response.Success);
            Assert.NotNull(response.Data);
        }

        [Fact]
        public async Task GetUsersBeNoContent()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.GetUsers()).ReturnsAsync((List<User>)null); // Simular respuesta nula
            var usersController = new UsersController(mockUserService.Object, null);

            // Action
            var result = await usersController.GetAllUsers();

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task RegisterCustomer_ValidUser_ReturnsOkResult()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.RegisterCustomer(It.IsAny<UserRegister>()))
                .ReturnsAsync(1); // Puedes ajustar este valor según tus necesidades

            var loggerMock = new Mock<ILogger<UsersController>>();
            var controller = new UsersController(userServiceMock.Object, loggerMock.Object);

            // Act
            var userRegister = new UserRegister(); // Puedes inicializar con datos necesarios
            var result = await controller.RegisterCustomer(userRegister) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);

            var responseData = result.Value as ResponseBase;
            Assert.NotNull(responseData);
            Assert.True(responseData.Success);
            Assert.Equal("user register", responseData.Message);
            Assert.NotNull(responseData.Data);

            var idUser = (int)responseData.Data.GetType().GetProperty("IdUser").GetValue(responseData.Data);
            Assert.Equal(1, idUser); // Ajusta este valor según la configuración del servicio mock
        }

        [Fact]
        public async Task RegisterCustomer_UserRegistrationFails_ReturnsBadRequest()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.RegisterCustomer(It.IsAny<UserRegister>()))
                .ReturnsAsync(0); // Indica que la registración falló

            var loggerMock = new Mock<ILogger<UsersController>>();
            var controller = new UsersController(userServiceMock.Object, loggerMock.Object);

            // Act
            var userRegister = new UserRegister(); // Puedes inicializar con datos necesarios
            var result = await controller.RegisterCustomer(userRegister) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}