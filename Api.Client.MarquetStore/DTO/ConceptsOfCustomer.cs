using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.DTO
{
    public class ConceptsOfCustomer
    {
        public int ConceptId { get; set; }

        public int SaleId { get; set; }

        public InformationProducts Product { get; set; }

        public decimal Import { get; set; }

        public int Quantity { get; set; }
        public List<PersonalizationsOfCustomer> Personalizations { get; set; }
    }
}
