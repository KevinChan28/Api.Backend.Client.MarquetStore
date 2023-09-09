using Api.Client.MarquetStore.Models.Others;
using Api.Client.MarquetStore.Models;
using Api.Client.MarquetStore.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Api.Client.MarquetStore.DTO;
using Microsoft.AspNetCore.Cors;

namespace Api.Client.MarquetStore.Controllers
{
    [EnableCors("Cors")]
    [Route("[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientsService _ingredientsService;

        public IngredientsController(IIngredientsService ingredientsService)
        {
            _ingredientsService = ingredientsService;
        }

        /// <summary>
        /// Registrar un ingrediente
        /// </summary>
        /// <returns>Id del ingrediente nuevo</returns>
        /// <response code="200"> Exito </response>
        /// <response code="500">Ha ocurrido un error en la creación.</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterIngredient([FromBody] IngredientRegister ingredientNew)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                if (ingredientNew == null)
                {
                    response.Success = false;
                    response.Message = "Ingredient not";
                    return BadRequest();
                }
                int idIngredient = await _ingredientsService.RegisterIngredient(ingredientNew);
                if (idIngredient > 0)
                {
                    response.Success = true;
                    response.Message = "Ingredient register";
                    response.Data = new { idIngredient = idIngredient };
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
        /// Obtener un ingrediente a partir de su ID
        /// </summary>
        /// <param name=""></param>
        /// <returns>Buscar ingrediente </returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = DataRoles.ADMINISTRATOR)]
        public async Task<IActionResult> GetIngredientById(int id)
        {
            ResponseBase answer = new ResponseBase();
            try
            {

                if (id < 1) 
                {
                    answer.Success = false;
                    answer.Message = "Ingredient ID is invalid"; // 
                    return BadRequest(answer); 
                }
                Ingredient idIngredient = await _ingredientsService.GetIngredientById(id);
                if (idIngredient != null) 
                {
                    answer.Success = true;
                    answer.Message = "Search ingredient";
                    answer.Data = idIngredient;
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
        /// <summary>
        /// Obtener todos los ingredientes
        /// </summary>
        /// <param name=""></param>
        /// <returns>Catalogo de ingredientes</returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = DataRoles.ADMINISTRATOR)]
        public async Task<IActionResult> GetIngredientsAvaliables()
        {
            ResponseBase answer = new ResponseBase();
            try
            {
                List<Ingredient> ingredients = await _ingredientsService.GetIngredientsAvaliables();

                if (ingredients != null)
                {
                    answer.Success = true;
                    answer.Message = "Search succes";
                    answer.Data = ingredients;
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
