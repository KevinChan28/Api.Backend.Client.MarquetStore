﻿using Api.Client.MarquetStore.DTO;

namespace Api.Client.MarquetStore.Service
{
    public interface IAddressService
    {
        Task<int> RegisterAddres(AddressRegister model);
        Task<List<AddressCustomer>> GetAddressOfCustomerById(int idCustomer);
    }
}
