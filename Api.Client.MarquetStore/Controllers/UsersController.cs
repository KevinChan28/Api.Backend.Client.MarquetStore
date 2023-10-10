using Api.Client.MarquetStore.Context;
using Api.Client.MarquetStore.DTO;
using Api.Client.MarquetStore.Models;
using Api.Client.MarquetStore.Models.Others;
using Api.Client.MarquetStore.Security;
using Api.Client.MarquetStore.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Org.BouncyCastle.Bcpg;
using System.Net;

namespace Api.Client.MarquetStore.Controllers
{
    [EnableCors("Cors")]
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
       private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        /// Registrar un cliente
        /// </summary>
        /// <returns>Id del usuario nuevo</returns>
        /// <response code="200"> Exito </response>
        /// <response code="500">Ha ocurrido un error en la creación.</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterCustomer([FromBody] UserRegister userNew)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                if (userNew == null)
                {
                    response.Success = false;
                    response.Message = "User not";
                    return BadRequest();
                }
                int IdUser = await _userService.RegisterCustomer(userNew);
                if (IdUser > 0)
                {
                    response.Success = true;
                    response.Message = "user register";
                    response.Data = new { IdUser = IdUser };
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    response.Message = ex.Message); ;
            }
            return Ok(response);
        }

        /// <summary>
        /// Validar credenciales del Login
        /// </summary>
        /// <returns>Token e información</returns>
        /// <response code="200"> Exito </response>
        /// <response code="500">Ha ocurrido un error en la verificación.</response>
        [HttpPost("Login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                if (login == null)
                {
                    response.Success = false;
                    response.Message = "Data not valid";
                    return BadRequest();
                }
                UserTokens validUser = await _userService.ValidateCredentials(login);
                if (validUser != null)
                {
                    response.Success = true;
                    response.Message = "user finded";
                    response.Data = validUser;
                }
                else
                {
                    return BadRequest(response.Message = "Credentials incorrect");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    response.Message = ex.Message); ;
            }
            return Ok(response);
        }

        /// <summary>
        /// Obtener todos los usuarios
        /// </summary>
        /// <param name=""></param>
        /// <returns>Catalogo de usuarios</returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
       //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = DataRoles.ADMINISTRATOR)]
        public async Task<IActionResult> GetAllUsers()
        {
            ResponseBase answer = new ResponseBase();
            try
            {
                List<User> users = await _userService.GetUsers();

                if (users != null)
                {
                    answer.Success = true;
                    answer.Message = "Search succes";
                    answer.Data = users;
                }
                else
                {
                    answer.Success = false;
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return Ok(answer);
        }

        /// <summary>
        /// Validar si ya existe el email
        /// </summary>
        /// <param name=""></param>
        /// <returns>booleano</returns>
        [HttpGet("validate/email/{email}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = DataRoles.ADMINISTRATOR)]
        public async Task<IActionResult> ValidateEmail([FromRoute] string email)
        {
            ResponseBase answer = new ResponseBase();
            try
            {
                ArgumentNullException.ThrowIfNull(email);
                    bool existEmail = await _userService.ValidateEmail(email);
                    answer.Success = true;
                    answer.Data = new {EmailExist = existEmail};
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return Ok(answer);
        }
    }
}
