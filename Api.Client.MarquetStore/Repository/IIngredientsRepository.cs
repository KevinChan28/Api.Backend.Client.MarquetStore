using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.Repository
{
    public interface IIngredientsRepository
    {
        Task<int> RegisterIngredient(Ingredient ingredient);
        Task<Ingredient> GetIngredientById(int idIngredient);
        Task<List<Ingredient>> GetIngrendientsAvaliables();
    }
}
