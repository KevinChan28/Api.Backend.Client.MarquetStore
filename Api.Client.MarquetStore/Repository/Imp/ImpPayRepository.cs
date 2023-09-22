using Api.Client.MarquetStore.Context;
using Api.Client.MarquetStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Api.Client.MarquetStore.Repository.Imp
{
    public class ImpPayRepository : IPayRepository
    {
        MarquetstoreDbContext _dbContext;

        public ImpPayRepository(MarquetstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<Pay>> GetPays()
        {
            return await _dbContext.Pays.AsNoTracking().ToListAsync();
        }

        public async Task<int> Register(Pay pay)
        {
            EntityEntry<Pay> payNew = await _dbContext.Pays.AddAsync(pay);
            await _dbContext.SaveChangesAsync();

            return payNew.Entity.Id;
        }
    }
}
