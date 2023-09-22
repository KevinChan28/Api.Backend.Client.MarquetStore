using Api.Client.MarquetStore.DTO;
using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.Service
{
    public interface IPayService
    {

        Task<int> RegisterPay(PayRegister pay);
        Task<List<Pay>> GetPays();
    }
}
