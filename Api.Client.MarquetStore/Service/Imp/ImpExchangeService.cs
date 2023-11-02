using Api.Client.MarquetStore.DTO;
using Api.Client.MarquetStore.Models;
using Api.Client.MarquetStore.Repository;

namespace Api.Client.MarquetStore.Service.Imp
{
    public class ImpExchangeService : IExchangeService
    {
        private IExchangeRepository _exchangeRepository;
        private ICouponRepository _couponRepository;
        private ISaleRepository _saleRepository;

        public ImpExchangeService(IExchangeRepository exchangeRepository, ICouponRepository couponRepository, ISaleRepository saleRepository)
        {
            _exchangeRepository = exchangeRepository;
            _couponRepository = couponRepository;
            _saleRepository = saleRepository;
        }

        public async Task<List<CouponsOfCustomer>> GetAllExchangesOfCustomer(int idCustomer)
        {
            List<Exchange> exchanges = await _exchangeRepository.GetExchanges();
            List<Exchange> couponsOfCustomer = exchanges.Where(x => x.UserId == idCustomer && x.IsUsed == false).ToList();   
            List<Coupon> coupons = await _couponRepository.GetAllCoupons();
            List<CouponsOfCustomer> view = couponsOfCustomer.Select(z => new CouponsOfCustomer
            {
                Id = z.Id,
                Coupon = coupons.Where(c => c.Id == z.CouponId).FirstOrDefault(),
                ExpiredDate = z.ExpiredDate,
                Count = z.Count,
                IsExpired = z.ExpiredDate > DateTime.Now ? false : true
            }).ToList();

            return view;
        }

        public async Task<int> GiveCouponToCustomer(int idCustomer)
        {
            List<Exchange> exchanges = await _exchangeRepository.GetExchanges();
            List<Exchange> exchangesOfCustomer =  exchanges.Where(z => z.UserId == idCustomer).ToList();
            List<Coupon> coupons = await _couponRepository.GetAllCoupons();
            List<Sale> salesOfCustomer = await _saleRepository.GetSalesOfCustomer(idCustomer);
            int quantitySales = salesOfCustomer.Count();

            if (coupons != null && salesOfCustomer != null)
            {
                bool haveSalesSufficient = coupons.Any(x => x.Quantity == quantitySales);

                if (haveSalesSufficient)
                {
                    Coupon coupon = coupons.Where(z => z.Quantity == quantitySales).FirstOrDefault();
                    Exchange exchange = new Exchange
                    {
                        UserId = idCustomer,
                        CouponId = coupon.Id,
                        ExpiredDate = DateTime.Now.AddDays(1),
                        IsUsed = false,
                        Count = exchangesOfCustomer.Count() + 1,
                    };
                    int idExchange = await _exchangeRepository.Register(exchange);

                    return idExchange;
                }
            }

            return 0;
        }
    }
}
