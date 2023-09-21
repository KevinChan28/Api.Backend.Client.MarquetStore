using System.ComponentModel.DataAnnotations;

namespace Api.Client.MarquetStore.DTO
{
    public class PersonalizationRegister
    {
        [Required]
        public int IngredientId { get; set; }
    }
}
