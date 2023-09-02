using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Client.MarquetStore.Models
{
    [Table("Address")]
    public class Address
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string InteriorNumber { get; set; }
        public string OutdoorNumber { get; set; }
        public int UserId { get; set; }
    }
}
