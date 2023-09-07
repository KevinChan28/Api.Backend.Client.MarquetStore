using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.Repository
{
    public interface ISaleRepository
    {
        Task<int> Register(Sale sale);
        Task<List<Sale>> GetSalesOfCustomer(int idCustomer);
    }
}
