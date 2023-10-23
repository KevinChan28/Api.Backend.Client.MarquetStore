namespace Api.Client.MarquetStore.Models
{
    public class Exchange
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CouponId { get; set; }
        public DateTime ExpiredDate { get; set; }
        public bool IsUsed { get; set; }
        public int Count { get; set; }
    }
}
