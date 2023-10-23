namespace Api.Client.MarquetStore.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Duration { get; set; }
        public float Discount { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}
