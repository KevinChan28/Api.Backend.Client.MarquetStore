using System.ComponentModel.DataAnnotations;

namespace Api.Client.MarquetStore.DTO
{
    public class AddressRegister
    {
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Neighborhood { get; set; }

        public string? InteriorNumber { get; set; }
        [Required]
        public string OutdoorNumber { get; set; }
        [Required]
        public string References { get; set; }

        public int UserId { get; set; }
    }
}
