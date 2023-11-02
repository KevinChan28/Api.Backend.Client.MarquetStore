using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.DTO
{
    public class CouponsOfCustomer
    {
        public int IdExchange { get; set; }
        public Coupon Coupon { get; set; }
        public DateTime ExpiredDate { get; set; }
        public int Count { get; set; }
        public bool IsExpired { get; set; }
    }
}
