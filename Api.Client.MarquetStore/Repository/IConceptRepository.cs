using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.Repository
{
    public interface IConceptRepository
    {
        Task<int> Register(Concept concept);
    }
}
