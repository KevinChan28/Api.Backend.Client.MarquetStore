using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Client.MarquetStore.Models
{
    [Table("Sale")]
    public class Sale
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Total { get; set; }
        public bool IsDelivered { get; set; }
        public int UserId { get; set; }
    }
}
