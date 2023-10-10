using System.ComponentModel.DataAnnotations;

namespace Api.Client.MarquetStore.DTO
{
    public class IngredientRegister
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public int? Stock { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool? IsAvailable { get; set; }
        [Required]
        public string? PathImage { get; set; }
    }
}
