using Api.Client.MarquetStore.Context;
using Api.Client.MarquetStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Client.MarquetStore.Repository.Imp
{
    public class ImpCouponRepository : ICouponRepository
    {
        MarquetstoreDbContext _context;

        public ImpCouponRepository(MarquetstoreDbContext context)
        {
            _context = context;
        }


        public async Task<List<Coupon>> GetAllCoupons()
        {
            return await _context.Coupons.AsNoTracking<Coupon>().ToListAsync();
        }
    }
}
