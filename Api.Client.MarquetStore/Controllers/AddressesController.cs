using Api.Client.MarquetStore.DTO;
using Api.Client.MarquetStore.Models.Others;
using Api.Client.MarquetStore.Models;
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
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }



        /// <summary>
        /// Registrar direccion
        /// </summary>
        /// <returns>Id de la direcion nueva</returns>
        /// <response code="200"> Exito </response>
        /// <response code="500">Ha ocurrido un error en la creación.</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterAddress([FromBody] AddressRegister model)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                if (model == null)
                {
                    response.Success = false;
                    response.Message = "Address not";
                    return BadRequest();
                }

                int idAddress = await _addressService.RegisterAddres(model);

                if (idAddress > 0)
                {
                    response.Success = true;
                    response.Message = "Address register";
                    response.Data = new { IdAddress = idAddress };
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
        /// Obtener direcciones de un cliente por su Id
        /// </summary>
        /// <param name="idCustomer"></param>
        /// <returns> información de la direccion </returns>
        [HttpGet("{idCustomer}")]
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
                    answer.Message = "Customer ID is invalid";
                    return BadRequest(answer);
                }
                List<AddressCustomer> addresses = await _addressService.GetAddressOfCustomerById(idCustomer);

                if (addresses != null)
                {
                    answer.Success = true;
                    answer.Message = "Search ingredient";
                    answer.Data = addresses;
                }
                else
                {
                    answer.Success = false;
                    answer.Message = "Ingredient not";
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
