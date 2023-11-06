using System.ComponentModel.DataAnnotations;

namespace Api.Client.MarquetStore.DTO
{
    public class AddressCustomer
    {
        [Required]
        public int Id { get; set; }

        public string ZipCode { get; set; } = null!;

        public string Street { get; set; } = null!;

        public string Neighborhood { get; set; } = null!;

        public string? InteriorNumber { get; set; } = null!;

        public string OutdoorNumber { get; set; }

        public string References { get; set; }
    }
}
