using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.Repository
{
    public interface IPaymentsMethodRepository
    {
        Task<List<PaymentsMethod>> GetPaymentsMethods();
        Task<int> RegisterPaymentMethod(PaymentsMethod paymentMethod);
    }
}
