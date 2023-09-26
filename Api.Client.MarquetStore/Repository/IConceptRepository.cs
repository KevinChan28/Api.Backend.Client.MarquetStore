using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.Repository
{
    public interface IConceptRepository
    {
        Task<int> Register(Concept concept);
        Task<List<Concept>> GetAllConcepts();
        Task Update(Concept concept);
        Task<Concept> GetConceptById(int idConcept);
        Task<List<Concept>> GetSalesByIdSale(int idSale);
    }
}
