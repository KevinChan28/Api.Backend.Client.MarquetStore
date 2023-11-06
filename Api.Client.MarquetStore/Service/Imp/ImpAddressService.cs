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

        public async Task<bool> Delete(int idAddress)
        {
            Address addressFind = await _addressRepository.GetAddressById(idAddress);

            if (addressFind != null)
            {
                await _addressRepository.Delete(addressFind);

                return true;
            }

            return false;
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

        public async Task<int> Update(AddressCustomer model)
        {
            Address addressFind = await _addressRepository.GetAddressById(model.Id);

            if (addressFind != null)
            {
                addressFind.Street = model.Street;
                addressFind.Neighborhood = model.Neighborhood;
                addressFind.References = model.References;
                addressFind.ZipCode = model.ZipCode;
                addressFind.InteriorNumber = model.InteriorNumber;
                addressFind.OutdoorNumber = model.OutdoorNumber;

                int idAddress = await _addressRepository.Update(addressFind);

                return idAddress;
            }

            return 0;
        }
    }
}
