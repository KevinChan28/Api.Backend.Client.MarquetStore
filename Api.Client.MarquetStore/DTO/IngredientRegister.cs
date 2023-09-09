namespace Api.Client.MarquetStore.DTO
{
    public class IngredientRegister
    {
        public string? Name { get; set; }

        public int? Stock { get; set; }

        public decimal Price { get; set; }

        public bool? IsAvailable { get; set; }

        public string? PathImage { get; set; }
    }
}
