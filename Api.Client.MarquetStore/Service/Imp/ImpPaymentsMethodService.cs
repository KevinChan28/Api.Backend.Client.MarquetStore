using Api.Client.MarquetStore.Models;
using Api.Client.MarquetStore.Repository;

namespace Api.Client.MarquetStore.Service.Imp
{
    public class ImpPaymentsMethodService : IPaymentsMethodService
    {
        IPaymentsMethodRepository _paymentsMethodRepository;

        public ImpPaymentsMethodService(IPaymentsMethodRepository paymentsMethodRepository)
        {
            _paymentsMethodRepository = paymentsMethodRepository;
        }

        public async Task<List<PaymentsMethod>> GetPaymentsMethods()
        {
            return await _paymentsMethodRepository.GetPaymentsMethods();
        }

        public async Task<int> RegisterPaymentMethod(PaymentsMethod paymentMethod)
        {
            PaymentsMethod paymentNew = new PaymentsMethod
            {
                Description = paymentMethod.Description
            };
            int idPaymentMethod = await _paymentsMethodRepository.RegisterPaymentMethod(paymentNew);

            return idPaymentMethod;
        }
    }
}
