using Api.Client.MarquetStore.DTO;
using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.Service
{
    public interface IExchangeService
    {
        Task<CouponReceived> GiveCouponToCustomer(int idCustomer);
        Task<List<CouponsOfCustomer>> GetAllExchangesOfCustomer(int idCustomer);
        Task<bool> CouponUsed(int idCoupon, int idCustomer);
    }
}
