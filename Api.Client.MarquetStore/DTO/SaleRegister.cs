using Api.Client.MarquetStore.Models;
using System.ComponentModel.DataAnnotations;

namespace Api.Client.MarquetStore.DTO
{
    public class SaleRegister
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public decimal Total { get; set; }
        [Required]
        public List<ConceptRegister> Concepts { get; set; }
        public SaleRegister()
        {
            this.Concepts = new List<ConceptRegister>();
        }
    }
}
