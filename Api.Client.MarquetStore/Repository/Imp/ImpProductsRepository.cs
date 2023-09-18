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

        public async Task<decimal> GetPriceProduct(int idProduct)
        {
            decimal price = await _dbContext.Products.Select(p => p.Price).FirstOrDefaultAsync();

            return price;
        }

        public async Task<Product> GetProductById(int idProduct)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(a => a.Id == idProduct);
        }

        public async Task<List<Product>> GetProducts()
        {
            return  _dbContext.Products.AsEnumerable<Product>().ToList();
        }

        public async Task<int> RegisterProduct(Product product)
        {
            EntityEntry<Product> productNew = await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return productNew.Entity.Id;
        }

        public async Task<int> Update(Product product)
        {
            EntityEntry<Product> update = _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();

            return update.Entity.Id;
        }
    }
}
