using Api.Client.MarquetStore.DTO;
using Api.Client.MarquetStore.Models;
using Api.Client.MarquetStore.Models.Others;
using Api.Client.MarquetStore.Repository;
using Api.Client.MarquetStore.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
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

        public CustomerController(ISaleService saleService)
        {
            _saleService = saleService;
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
    }
}
