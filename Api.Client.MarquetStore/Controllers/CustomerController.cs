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
        private readonly IExchangeService _exchangeService;

        public CustomerController(ISaleService saleService, IUserService userService, IExchangeService exchangeService)
        {
            _saleService = saleService;
            _userService = userService;
            _exchangeService = exchangeService;
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

        /// <summary>
        /// Obtener los cupones de un cliente por su Id
        /// </summary>
        /// <param name="idCustomer"></param>
        /// <returns> información del ingrediente </returns>
        [HttpGet("Exchange/{idCustomer}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = DataRoles.ADMINISTRATOR)]
        public async Task<IActionResult> GetIngredientById([FromRoute] int idCustomer)
        {
            ResponseBase answer = new ResponseBase();
            try
            {

                if (idCustomer < 1)
                {
                    answer.Success = false;
                    answer.Message = "Customer Id is invalid";
                    return BadRequest(answer);
                }

                List<CouponsOfCustomer> couponsOfCusomer = await _exchangeService.GetAllExchangesOfCustomer(idCustomer);

                if (couponsOfCusomer != null)
                {
                    answer.Success = true;
                    answer.Message = "Search success";
                    answer.Data = couponsOfCusomer;
                }
                else
                {
                    answer.Success = false;
                    answer.Message = "coupons not";
                    return NotFound(answer);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return Ok(answer);
        }

        /// <summary>
        /// Modificar que el cupon ya fue usado
        /// </summary>
        /// <param name="idCustomer"></param>
        /// <param name="idCoupon"></param>
        /// <returns> información del ingrediente </returns>
        [HttpPut("Exchange/{idCustomer}/{idCoupon}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = DataRoles.ADMINISTRATOR)]
        public async Task<IActionResult> GetIngredientById([FromRoute] int idCustomer, int idCoupon)
        {
            ResponseBase answer = new ResponseBase();
            bool updated;
            try
            {

                if (idCustomer < 1 && idCoupon < 1)
                {
                    answer.Success = false;
                    answer.Message = "Customer Id or Coupon Id is invalid";
                    return BadRequest(answer);
                }

                    updated = await _exchangeService.CouponUsed(idCustomer, idCoupon);
                    answer.Success = updated;
                    answer.Message = "Updated success";
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return updated == true ? Ok(answer) : NotFound("Failed");
        }
    }
}
