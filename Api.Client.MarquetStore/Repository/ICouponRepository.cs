using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.Repository
{
    public interface ICouponRepository
    {
        Task<List<Coupon>> GetAllCoupons();
        Task<Coupon> GetCouponById(int couponId);
        Task UpdateCoupon(Coupon coupon);
    }
}
