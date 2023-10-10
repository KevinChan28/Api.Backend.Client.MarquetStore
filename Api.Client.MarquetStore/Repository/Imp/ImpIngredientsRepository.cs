using Api.Client.MarquetStore.Context;
using Api.Client.MarquetStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
namespace Api.Client.MarquetStore.Repository.Imp
{
    public class ImpIngredientsRepository : IIngredientsRepository
    {
        MarquetstoreDbContext _dbContext;

        public ImpIngredientsRepository(MarquetstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> RegisterIngredient(Ingredient ingredient)
        {
            EntityEntry<Ingredient> ingredientNew = await _dbContext.Ingredients.AddAsync(ingredient);
            await _dbContext.SaveChangesAsync();

            return ingredientNew.Entity.Id;
        }

        public async Task<Ingredient> GetIngredientById(int idIngredient)
        {
            return await _dbContext.Ingredients.FirstOrDefaultAsync(i => i.Id == idIngredient);
        }

        public async Task<List<Ingredient>> GetIngrendientsAvailables()
        {
            return _dbContext.Ingredients.Where(i => i.IsAvailable == true).ToList();
        }
    }
}
