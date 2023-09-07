using Api.Client.MarquetStore.DTO;
using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.Service
{
    public interface ISaleService
    {
        Task<int> RegisterSale(SaleRegister model);
        Task<List<SalesOfCustomer>> GetSalesOfCustomer(int idCustomer);
    }
}
