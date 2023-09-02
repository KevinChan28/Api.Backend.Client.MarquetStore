using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Client.MarquetStore.Models
{
    [Table("Ingredients")]
    public class Ingredients
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public string? PathImage { get; set; }
    }
}
