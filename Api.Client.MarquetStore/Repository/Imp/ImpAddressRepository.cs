using Api.Client.MarquetStore.Context;
using Api.Client.MarquetStore.DTO;
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

        public async Task Delete(Address address)
        {
            EntityEntry<Address> delete = _dbContext.Addresses.Remove(address);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Address> GetAddressById(int idAddress)
        {
            return _dbContext.Addresses.Where(a => a.Id == idAddress).FirstOrDefault();
        }

        public async Task<List<Address>> GetAllAdresses()
        {
            return _dbContext.Addresses.OrderByDescending(x => x.Id).AsEnumerable<Address>().ToList();
        }

        public async Task<int> Register(Address address)
        {
           EntityEntry<Address> addressNew = await _dbContext.Addresses.AddAsync(address);
            await _dbContext.SaveChangesAsync();

            return addressNew.Entity.Id;
        }

        public async Task<int> Update(Address addressCustomer)
        {
            EntityEntry<Address> update = _dbContext.Addresses.Update(addressCustomer);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
           

            return update.Entity.Id;
        }
    }
}
