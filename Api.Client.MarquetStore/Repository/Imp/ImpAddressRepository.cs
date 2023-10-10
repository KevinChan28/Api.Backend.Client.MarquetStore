using Api.Client.MarquetStore.Context;
using Api.Client.MarquetStore.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Api.Client.MarquetStore.Repository.Imp
{
    public class ImpAddressRepository : IAddressRepository
    {
        MarquetstoreDbContext _dbContext;

        public ImpAddressRepository(MarquetstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<Address>> GetAllAdresses()
        {
            return _dbContext.Addresses.AsEnumerable<Address>().ToList();
        }

        public async Task<int> Register(Address address)
        {
           EntityEntry<Address> addressNew = await _dbContext.Addresses.AddAsync(address);
            await _dbContext.SaveChangesAsync();

            return addressNew.Entity.Id;
        }
    }
}
