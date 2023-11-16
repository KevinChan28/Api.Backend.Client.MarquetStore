using Api.Client.MarquetStore.Models.Others;
using Api.Client.MarquetStore.Models;
using Api.Client.MarquetStore.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Api.Client.MarquetStore.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Api.Client.MarquetStore.Controllers
{
    [EnableCors("Cors")]
    [Route("[controller]")]
    [ApiController]
    public class PaymentsMethodController : ControllerBase
    {
        private readonly IPaymentsMethodService _paymentsMethodService;

        public PaymentsMethodController(IPaymentsMethodService paymentsMethodService)
        {
            _paymentsMethodService = paymentsMethodService;
        }

        /// <summary>
        /// Obtener todos los metodos de pago
        /// </summary>
        /// <param name=""></param>
        /// <returns> catalogo de metodos de pagos </returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = DataRoles.ADMINISTRATOR)]
        public async Task<IActionResult> GetAllPaymentsMethod()
        {
            ResponseBase answer = new ResponseBase();
            try
            {
                List<PaymentsMethod> payments = await _paymentsMethodService.GetPaymentsMethods();
                if (payments != null)
                {
                    answer.Success = true;
                    answer.Message = "Search Payments method";
                    answer.Data = payments;
                }
                else
                {
                    answer.Success = false;
                    answer.Message = "Payments Method not";
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
        /// Registrar un metodo de pago
        /// </summary>
        /// <returns>Id del método de pago nuevo</returns>
        /// <response code="200"> Exito </response>
        /// <response code="500">Ha ocurrido un error en la creación.</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterPaymentMethod([FromBody] PaymentsMethod paymentMethodNew)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                if (paymentMethodNew == null)
                {
                    response.Success = false;
                    response.Message = "Payment method not";
                    return BadRequest();
                }

                int idPaymentMethod = await _paymentsMethodService.RegisterPaymentMethod(paymentMethodNew);

                if (idPaymentMethod > 0)
                {
                    response.Success = true;
                    response.Message = "Payment method register";
                    response.Data = new { idPaymentMethod = idPaymentMethod };
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
    }
}
