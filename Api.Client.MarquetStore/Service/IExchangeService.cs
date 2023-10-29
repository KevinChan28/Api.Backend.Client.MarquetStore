using Api.Client.MarquetStore.DTO;
using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.Service
{
    public interface IExchangeService
    {
        Task<int> GiveCouponToCustomer(int idCustomer);
        Task<List<CouponsOfCustomer>> GetAllExchangesOfCustomer(int idCustomer);
    }
}
