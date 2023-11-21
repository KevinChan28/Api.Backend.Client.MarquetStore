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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = DataRoles.CUSTOMER)]
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
        /// Actualizar datos de la direccion
        /// </summary>
        /// <returns> información de la direccion </returns>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = DataRoles.CUSTOMER)]
        public async Task<IActionResult> UpdateAddress([FromBody] AddressCustomer addressCustomer)
        {
            ResponseBase answer = new ResponseBase();
            try
            {
            
                int idAddress = await _addressService.Update(addressCustomer);

                if (idAddress > 0)
                {
                    answer.Success = true;
                    answer.Message = "Address updated";
                    answer.Data = idAddress;
                }
                else
                {
                    answer.Success = false;
                    answer.Message = "Ingredient not";
                    return BadRequest(answer);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return Ok(answer);
        }

        /// <summary>
        /// Eliminar direccion
        /// </summary>
        /// <returns>booleano</returns>
        /// <response code="200"> Exito </response>
        /// <response code="500">Ha ocurrido un error en la creación.</response>
        [HttpDelete("{idAddress}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = DataRoles.CUSTOMER)]
        public async Task<IActionResult> DeleteAddress([FromRoute] int idAddress)
        {
            ResponseBase response = new ResponseBase();
            try
            {

                response.Success = await _addressService.Delete(idAddress);


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    response.Message = ex.Message); ;
            }

            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// Obtener las direcciones de un cliente
        /// </summary>
        /// <returns>Lista de objetos Addrwss</returns>
        [HttpGet("User/{idCustomer}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = DataRoles.CUSTOMER)]
        public async Task<IActionResult> GetAddressOfCustomer([FromRoute]int idCustomer)
        {
            ResponseBase answer = new ResponseBase();
            try
            {
                List<AddressCustomer> addresses = await _addressService.GetAddressOfCustomerById(idCustomer);

                if (addresses != null)
                {
                    answer.Success = true;
                    answer.Message = "Search success";
                    answer.Data = addresses;
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
