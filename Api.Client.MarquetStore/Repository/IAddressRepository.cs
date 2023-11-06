using Api.Client.MarquetStore.DTO;
using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.Repository
{
    public interface IAddressRepository
    {
        Task<int> Register(Address address);
        Task<List<Address>> GetAllAdresses();
        Task<int> Update(Address addressCustomer);
        Task Delete(Address address);
        Task<Address> GetAddressById(int idAddress);
    }
}
