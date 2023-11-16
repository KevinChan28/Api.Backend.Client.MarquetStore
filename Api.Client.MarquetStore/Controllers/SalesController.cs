using Api.Client.MarquetStore.DTO;
using Api.Client.MarquetStore.Models.Others;
using Api.Client.MarquetStore.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Client.MarquetStore.Controllers
{
    [EnableCors("Cors")]
    [Route("[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
       private readonly ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }


        /// <summary>
        /// Registrar una compra
        /// </summary>
        /// <returns>Id de la compra nueva</returns>
        /// <response code="200"> Exito </response>
        /// <response code="500">Ha ocurrido un error en la creación.</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = DataRoles.CUSTOMER)]
        public async Task<IActionResult> RegisterSale([FromBody] SaleRegister model)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                if (model == null)
                {
                    response.Success = false;
                    response.Message = "User not";
                    return BadRequest();
                }
                int IdSale = await _saleService.RegisterSale(model);
                if (IdSale > 0)
                {
                    response.Success = true;
                    response.Message = "SALE register";
                    response.Data = new { IdSale = IdSale };
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
