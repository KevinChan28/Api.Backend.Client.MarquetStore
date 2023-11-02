using Api.Client.MarquetStore.Context;
using Api.Client.MarquetStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

        public async Task<Coupon> GetCouponById(int couponId)
        {
            return _context.Coupons.Where(x => x.Id == couponId).FirstOrDefault();
        }

        public async Task UpdateCoupon(Coupon coupon)
        {
            EntityEntry<Coupon> updated = _context.Coupons.Update(coupon);
            await _context.SaveChangesAsync();
        }
    }
}
