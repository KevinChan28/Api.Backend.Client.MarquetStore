using Api.Client.MarquetStore.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Api.Client.MarquetStore.Repository.Imp
{
    public class ImpDatabaseRepository : IDatabaseRepository
    {
        MarquetstoreDbContext _dbContext;

        public ImpDatabaseRepository(MarquetstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IDbContextTransaction> BeginTransaction()
        {
           return await _dbContext.Database.BeginTransactionAsync();
        }
    }
}
