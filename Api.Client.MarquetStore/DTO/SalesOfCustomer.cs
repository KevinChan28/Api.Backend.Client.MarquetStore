using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.DTO
{
    public class SalesOfCustomer
    {
        public int IdSale{ get; set; }

        public DateTime CreatedDate { get; set; }

        public decimal Total { get; set; }

        public bool? IsDelivered { get; set; }
        public List<ConceptsOfCustomer> Concepts { get; set; }
    }
}
