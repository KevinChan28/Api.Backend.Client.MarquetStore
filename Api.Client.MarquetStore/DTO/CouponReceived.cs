namespace Api.Client.MarquetStore.DTO
{
    public class CouponReceived
    {
        public string Code { get; set; }
        public DateTime ExpiredDate { get; set; }
        public float Discount { get; set; }
        public string Description { get; set; }
    }
}
