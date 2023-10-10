using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.Repository
{
    public interface IAddressRepository
    {
        Task<int> Register(Address address);
        Task<List<Address>> GetAllAdresses();
    }
}
