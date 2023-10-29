using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.Repository
{
    public interface IExchangeRepository
    {
        Task<int> Register(Exchange exchange);
        Task<List<Exchange>> GetExchanges();
    }
}
