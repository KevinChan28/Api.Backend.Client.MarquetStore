namespace Api.Client.MarquetStore.DTO
{
    public class ProductRegister
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public bool? IsAvailable { get; set; }

        public int Stock { get; set; }

        public string Pathlmage { get; set; } = null!;
    }
}
