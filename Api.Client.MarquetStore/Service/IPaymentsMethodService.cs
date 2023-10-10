using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.Service
{
    public interface IPaymentsMethodService
    {
        Task<List<PaymentsMethod>> GetPaymentsMethods();
    }
}
