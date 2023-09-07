using Api.Client.MarquetStore.Context;
using Api.Client.MarquetStore.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Api.Client.MarquetStore.Repository.Imp
{
    public class ImpPersonalizationRepository : IPersonalizationRepository
    {
        MarquetstoreDbContext _dbContext;

        public ImpPersonalizationRepository(MarquetstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Personalization>> GetAllPersonalizations()
        {
            return _dbContext.Personalizations.AsEnumerable<Personalization>().ToList();
        }

        public async Task<int> Register(Personalization personalization)
        {
            EntityEntry<Personalization> newPersonalization = await _dbContext.Personalizations.AddAsync(personalization);
            await _dbContext.SaveChangesAsync();

            return newPersonalization.Entity.Id;
        }
    }
}
