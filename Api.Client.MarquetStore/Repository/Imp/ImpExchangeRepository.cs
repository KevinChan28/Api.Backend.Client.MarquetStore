using Api.Client.MarquetStore.Context;
using Api.Client.MarquetStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Api.Client.MarquetStore.Repository.Imp
{
    public class ImpExchangeRepository : IExchangeRepository
    {
        MarquetstoreDbContext _dbContext;

        public ImpExchangeRepository(MarquetstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<Exchange>> GetExchanges()
        {
            return await _dbContext.Exchanges.AsNoTracking<Exchange>().ToListAsync();
        }

        public async Task<int> Register(Exchange exchange)
        {
           EntityEntry<Exchange> newExchange = await _dbContext.Exchanges.AddAsync(exchange);
            await _dbContext.SaveChangesAsync();

            return newExchange.Entity.Id;
        }

        public async Task<int> Update(Exchange exchange)   
        {
            EntityEntry<Exchange> updadte = _dbContext.Exchanges.Update(exchange);
            await _dbContext.SaveChangesAsync();

            return updadte.Entity.Id;
        }
    }
}
