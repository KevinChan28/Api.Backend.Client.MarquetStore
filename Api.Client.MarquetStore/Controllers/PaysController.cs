using Api.Client.MarquetStore.DTO;
using Api.Client.MarquetStore.Models.Others;
using Api.Client.MarquetStore.Models;
using Api.Client.MarquetStore.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Api.Client.MarquetStore.Controllers
{
    [EnableCors("Cors")]
    [Route("[controller]")]
    [ApiController]
    public class PaysController : ControllerBase
    {
        private readonly IPayService _payService;

        public PaysController(IPayService payService)
        {
            _payService = payService;
        }


        /// <summary>
        /// Registrar un pago
        /// </summary>
        /// <returns>Id del pago nuevo</returns>
        /// <response code="200"> Exito </response>
        /// <response code="500">Ha ocurrido un error en la creación.</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterPay([FromBody] PayRegister model)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                if (model == null)
                {
                    response.Success = false;
                    response.Message = "Pay not";
                    return BadRequest();
                }

                int idPay = await _payService.RegisterPay(model);

                if (idPay > 0)
                {
                    response.Success = true;
                    response.Message = "Pay register";
                    response.Data = new { IdPay = idPay };
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
        /// Obtener todos los pagos
        /// </summary>
        /// <param name=""></param>
        /// <returns> catalogo de pagos </returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = DataRoles.ADMINISTRATOR)]
        public async Task<IActionResult> GetAllPays()
        {
            ResponseBase answer = new ResponseBase();
            try
            {
                List<Pay> pays = await _payService.GetPays();
                if (pays != null)
                {
                    answer.Success = true;
                    answer.Message = "Search Pays";
                    answer.Data = pays;
                }
                else
                {
                    answer.Success = false;
                    answer.Message = "Pays not";
                    return NotFound(answer);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return Ok(answer);
        }
    }
}
