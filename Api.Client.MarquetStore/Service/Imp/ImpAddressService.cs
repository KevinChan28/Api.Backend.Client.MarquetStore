using Api.Client.MarquetStore.DTO;
using Api.Client.MarquetStore.Models;
using Api.Client.MarquetStore.Repository;

namespace Api.Client.MarquetStore.Service.Imp
{
    public class ImpAddressService : IAddressService
    {
        private IAddressRepository _addressRepository;

        public ImpAddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }


        public async Task<List<AddressCustomer>> GetAddressOfCustomerById(int idCustomer)
        {
            List<Address> addresses = await _addressRepository.GetAllAdresses();
            List<AddressCustomer> customerAddress = addresses.Where(a => a.UserId == idCustomer).Select( x => new AddressCustomer
            {
                Id = x.Id,
                Street = x.Street,
                InteriorNumber = x.InteriorNumber,
                Neighborhood = x.Neighborhood,
                OutdoorNumber = x.OutdoorNumber,
                References = x.References,
                ZipCode = x.ZipCode,
            }).ToList();

            return customerAddress;
        }

        public async Task<int> RegisterAddres(AddressRegister model)
        {
            Address address = new Address
            {
                Street = model.Street,
                InteriorNumber = model.InteriorNumber,
                Neighborhood = model.Neighborhood,
                OutdoorNumber = model.OutdoorNumber,
                References = model.References,
                ZipCode = model.ZipCode,
                UserId = model.UserId,
            };
            int idAddress = await _addressRepository.Register(address);

            return idAddress;
        }
    }
}
