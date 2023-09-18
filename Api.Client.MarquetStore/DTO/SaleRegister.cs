using Api.Client.MarquetStore.Models;

namespace Api.Client.MarquetStore.DTO
{
    public class SaleRegister
    {

        public int UserId { get; set; }
        public List<ConceptRegister> Concepts { get; set; }
        public SaleRegister()
        {
            this.Concepts = new List<ConceptRegister>();
        }
    }
}
