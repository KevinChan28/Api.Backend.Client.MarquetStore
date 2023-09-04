using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.DTO
{
    public class ConceptRegister
    {
        public int Quantity { get; set; }

        public List<PersonalizationRegister> Personalizations { get; set; }
    }
}
