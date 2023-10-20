using Api.Client.MarquetStore.DTO;
using Api.Client.MarquetStore.Models;
using Api.Client.MarquetStore.Models.Others;
using Api.Client.MarquetStore.Security;
using Api.Client.MarquetStore.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Client.MarquetStore.Controllers
{
    [EnableCors("Cors")]
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly IUserService _userService;

        public CustomerController(ISaleService saleService, IUserService userService)
        {
            _saleService = saleService;
            _userService = userService;
        }

        /// <summary>
        /// Obtener todas las compras de un cliente
        /// </summary>
        /// <param name=""></param>
        /// <returns>Lista de objetos Sale</returns>
        [HttpGet("sales/{idCustomer}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = DataRoles.ADMINISTRATOR)]
        public async Task<IActionResult> GetAllProducts(int idCustomer)
        {
            ResponseBase answer = new ResponseBase();
            try
            {
                List<SalesOfCustomer> sales = await _saleService.GetSalesOfCustomer(idCustomer);

                if (sales != null)
                {
                    answer.Success = true;
                    answer.Message = "Search succes";
                    answer.Data = sales;
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
        /// Obtener un cliente por su id
        /// </summary>
        /// <param name="idCustomer"></param>
        /// <returns>Lista de objetos Sale</returns>
        [HttpGet("{idCustomer}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = DataRoles.ADMINISTRATOR)]
        public async Task<IActionResult> GetCustomerById(int idCustomer)
        {
            ResponseBase answer = new ResponseBase();
            try
            {
                User user = await _userService.GetUserById(idCustomer);

                if (user != null)
                {
                    answer.Success = true;
                    answer.Message = "Search succes";
                    answer.Data = user;
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
        /// Actualizar datos del cliente
        /// </summary>
        /// <returns>Id del cliente actualizado</returns>
        /// <response code="200"> Exito </response>
        /// <response code="500">Ha ocurrido un error en la verificación.</response>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateCustomer([FromBody] UserUpdate model)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                int idUser = await _userService.UpdateCustomer(model);

                if (idUser > 0)
                {
                    response.Success = true;
                    response.Message = "user updated";
                    response.Data = idUser;
                }
                else
                {
                    return BadRequest(response.Message = "Customer not updated");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    response.Message = ex.Message); ;
            }
            return Ok(response);
        }
    }
}
