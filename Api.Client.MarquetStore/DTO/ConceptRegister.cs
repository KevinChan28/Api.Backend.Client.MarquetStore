using Api.Client.MarquetStore.Models;
using Api.Client.MarquetStore.Repository;
using System.ComponentModel.DataAnnotations;

namespace Api.Client.MarquetStore.DTO
{
    public class ConceptRegister
    {
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int ProductId { get; set; }
        public List<PersonalizationRegister> Personalizations { get; set; }
    }
}
