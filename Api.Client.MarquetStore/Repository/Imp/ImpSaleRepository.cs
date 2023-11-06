using Api.Client.MarquetStore.Context;
using Api.Client.MarquetStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.FileSystemGlobbing;

namespace Api.Client.MarquetStore.Repository.Imp
{
    public class ImpSaleRepository : ISaleRepository
    {
        MarquetstoreDbContext _dbContext;

        public ImpSaleRepository(MarquetstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Sale> GetSaleById(int idSale)
        {
            return await _dbContext.Sales.FirstOrDefaultAsync(x => x.Id == idSale);
        }

        public async Task<List<Sale>> GetSalesOfCustomer(int idCustomer)
        {
            return _dbContext.Sales.OrderByDescending(x => x.Id).AsEnumerable<Sale>().Where(id => id.UserId == idCustomer).ToList();
        }

        public async Task<int> Register(Sale sale)
        {
            EntityEntry<Sale> saleNew = await _dbContext.Sales.AddAsync(sale);
            await _dbContext.SaveChangesAsync();

            return saleNew.Entity.Id;
        }

        public async Task UpdateSale(Sale sale)
        {
            EntityEntry<Sale> saleUpdate = _dbContext.Sales.Update(sale);
            await _dbContext.SaveChangesAsync();
        }
    }
}
