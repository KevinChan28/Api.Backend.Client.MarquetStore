using Api.Client.MarquetStore.DTO;
using Api.Client.MarquetStore.Models.Others;
using Api.Client.MarquetStore.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Client.MarquetStore.Controllers
{
    [EnableCors("Cors")]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = DataRoles.CUSTOMER)]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }


        /// <summary>
        /// Registrar un producto
        /// </summary>
        /// <returns>Id del producto nuevo</returns>
        /// <response code="200"> Exito </response>
        /// <response code="500">Ha ocurrido un error en la creación.</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterProduct([FromBody] ProductRegister productNew)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                if (productNew == null)
                {
                    response.Success = false;
                    response.Message = "product not";
                    return BadRequest();
                }
                int IdProduct = await _productsService.RegisterProduct(productNew);
                if (IdProduct > 0)
                {
                    response.Success = true;
                    response.Message = "product register";
                    response.Data = new { IdProduct = IdProduct };
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
        /// Obtener todos los productos disponibles
        /// </summary>
        /// <param name=""></param>
        /// <returns>Catalogo de productos</returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllProducts()
        {
            ResponseBase answer = new ResponseBase();
            try
            {
                ViewPrincipalProducts products = await _productsService.GetProducts();

                if (products != null)
                {
                    answer.Success = true;
                    answer.Message = "Search succes";
                    answer.Data = products;
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
