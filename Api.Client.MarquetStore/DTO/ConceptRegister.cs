using Api.Client.MarquetStore.Models;
using Api.Client.MarquetStore.Repository;
using System.ComponentModel.DataAnnotations;

namespace Api.Client.MarquetStore.DTO
{
    public class ConceptRegister
    {
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public decimal Import { get; set; } 

        public List<PersonalizationRegister> Personalizations { get; set; }
    }
}
