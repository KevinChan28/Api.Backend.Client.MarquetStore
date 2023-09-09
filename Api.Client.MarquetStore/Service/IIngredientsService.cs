using Api.Client.MarquetStore.DTO;
using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.Service
{
    public interface IIngredientsService
    {
        Task<int> RegisterIngredient(IngredientRegister model);

        Task<Ingredient> GetIngredientById(int idIngredient);

        Task<List<Ingredient>> GetIngredientsAvaliables();
    }
}
