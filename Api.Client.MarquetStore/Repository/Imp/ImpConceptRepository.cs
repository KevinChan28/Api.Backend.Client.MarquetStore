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

        public async Task<List<Concept>> GetAllConcepts()
        {
            return _dbContext.Concepts.AsEnumerable<Concept>().ToList();
        }

        public async Task<Concept> GetConceptById(int idConcept)
        {
            return _dbContext.Concepts.Where(a => a.Id == idConcept).FirstOrDefault();
        }

        public async Task<List<Concept>> GetSalesByIdSale(int idSale)
        {
            return _dbContext.Concepts.Where(x => x.SaleId == idSale).ToList();
        }

        public async Task<int> Register(Concept concept)
        {
            EntityEntry<Concept> conceptNew = await _dbContext.AddAsync(concept);
            await _dbContext.SaveChangesAsync();

            return conceptNew.Entity.Id;
        }

        public async Task Update(Concept concept)
        {
            EntityEntry<Concept> conceptUpdated = _dbContext.Concepts.Update(concept);
            await _dbContext.SaveChangesAsync();
        }
    }
}
