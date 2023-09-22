using Api.Client.MarquetStore.DTO;
using Api.Client.MarquetStore.Models;
using Api.Client.MarquetStore.Repository;

namespace Api.Client.MarquetStore.Service.Imp
{
    public class ImpPayService : IPayService
    {
        IPayRepository _payRepository;

        public ImpPayService(IPayRepository payRepository)
        {
            _payRepository = payRepository;
        }


        public async Task<List<Pay>> GetPays()
        {
           return await _payRepository.GetPays();
        }

        public async Task<int> RegisterPay(PayRegister pay)
        {
            Pay payNew = new Pay
            {
                SaleId = pay.SaleId,
                CreatedDate = DateTime.Now,
                PaymentsMethodId = pay.PaymentsMethodId,
            };
            int payId = await _payRepository.Register(payNew);

            return payId;
        }
    }
}
