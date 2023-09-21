using Api.Client.MarquetStore.DTO;
using Api.Client.MarquetStore.Models;
using Api.Client.MarquetStore.Repository;

namespace Api.Client.MarquetStore.Service.Imp
{
    public class ImpIngredientsService : IIngredientsService
    {
        IIngredientsRepository _ingredientsRepository;

        public ImpIngredientsService(IIngredientsRepository ingredientsRepository)
        {
            _ingredientsRepository = ingredientsRepository;
        }

        public async Task<int> RegisterIngredient(IngredientRegister model)
        {
            Ingredient ingredientNew = new Ingredient
            {
                Name = model.Name,
                IsAvailable = model.IsAvailable,
                Stock = model.Stock,
                Price = model.Price,
                PathImage = model.PathImage
            };
            int idIngredient = await _ingredientsRepository.RegisterIngredient(ingredientNew);

            return idIngredient;
        }

        public async Task<Ingredient> GetIngredientById(int idIngredient)
        {
            return await _ingredientsRepository.GetIngredientById(idIngredient);
        }

        public async Task<List<Ingredient>> GetIngredientsAvailables()
        {
            return await _ingredientsRepository.GetIngrendientsAvailables();
        }
    }
}
