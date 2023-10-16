using Api.Client.MarquetStore.Context;
using Api.Client.MarquetStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Api.Client.MarquetStore.Repository.Imp
{
    public class ImpPaymentsMethodRepository : IPaymentsMethodRepository
    {
        MarquetstoreDbContext _dbContext;

        public ImpPaymentsMethodRepository(MarquetstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PaymentsMethod>> GetPaymentsMethods()
        {
            return await _dbContext.PaymentsMethods.AsNoTracking().ToListAsync();
        }

        public async Task<int> RegisterPaymentMethod(PaymentsMethod paymentMethod)
        {
            EntityEntry<PaymentsMethod> paymentMethodNew = await _dbContext.PaymentsMethods.AddAsync(paymentMethod);
            await _dbContext.SaveChangesAsync();

            return paymentMethodNew.Entity.Id;
        }
    }
}
