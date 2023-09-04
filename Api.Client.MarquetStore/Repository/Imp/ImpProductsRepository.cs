using Api.Client.MarquetStore.Context;
using Api.Client.MarquetStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Api.Client.MarquetStore.Repository.Imp
{
    public class ImpProductsRepository : IProductsRepository
    {
        MarquetstoreDbContext _dbContext;

        public ImpProductsRepository(MarquetstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Product> GetProductById(int idProduct)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(a => a.Id == idProduct);
        }

        public async Task<int> Update(Product product)
        {
            EntityEntry<Product> update = _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();

            return update.Entity.Id;
        }
    }
}
