using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.DTO
{
    public class PersonalizationsOfCustomer
    {
        public int IdPersonalization { get; set; }

        public int ConceptId { get; set; }

        public int? Ingredients { get; set; }
    }
}
