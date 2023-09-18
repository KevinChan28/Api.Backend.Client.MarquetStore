using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.DTO
{
    public class ViewPrincipalProducts
    {
        public int TotalProduct { get; set; }
        public List<Product> Products { get; set; }
    }
}
