using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.Repository
{
    public interface IPayRepository
    {
        Task<int> Register(Pay pay);
        Task<List<Pay>> GetPays();
    }
}
