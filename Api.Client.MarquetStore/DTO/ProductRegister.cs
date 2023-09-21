using System.ComponentModel.DataAnnotations;

namespace Api.Client.MarquetStore.DTO
{
    public class ProductRegister
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool? IsAvailable { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public string Pathlmage { get; set; } = null!;
    }
}
