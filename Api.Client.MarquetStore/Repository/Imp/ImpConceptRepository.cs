using Api.Client.MarquetStore.Context;
using Api.Client.MarquetStore.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Api.Client.MarquetStore.Repository.Imp
{
    public class ImpConceptRepository : IConceptRepository
    {
        MarquetstoreDbContext _dbContext;

        public ImpConceptRepository(MarquetstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<int> Register(Concept concept)
        {
            EntityEntry<Concept> conceptNew = await _dbContext.AddAsync(concept);
            await _dbContext.SaveChangesAsync();

            return conceptNew.Entity.Id;
        }
    }
}
