using Api.Client.MarquetStore.DTO;
using Api.Client.MarquetStore.Models;
using Api.Client.MarquetStore.Models.Others;
using Api.Client.MarquetStore.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Api.Client.MarquetStore.Controllers
{
    [EnableCors("Cors")]
    [Route("[controller]")]
    [ApiController]
    public class ExchangesController : ControllerBase
    {
        private readonly IExchangeService _exchangeService;

        public ExchangesController(IExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }

        /// <summary>
        /// Otorgar cupón a un cliente
        /// </summary>
        /// <returns>Id del canje</returns>
        /// <response code="200"> Exito </response>
        /// <response code="500">Ha ocurrido un error en la creación.</response>
        [HttpPost("{idCustomer}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = DataRoles.CUSTOMER)]
        public async Task<IActionResult> RegisterExchange([FromRoute] int idCustomer)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                if (idCustomer < 1)
                {
                    return BadRequest();
                }

                CouponReceived couponReceived = await _exchangeService.GiveCouponToCustomer(idCustomer);

                if (couponReceived != null)
                {
                    response.Success = true;
                    response.Message = "Coupon awarded";
                    response.Data = couponReceived;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Not has sufficient sales";
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
