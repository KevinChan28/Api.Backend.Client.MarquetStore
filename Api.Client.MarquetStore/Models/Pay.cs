using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Client.MarquetStore.Models
{
    [Table("Pay")]
    public class Pay
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public int SaleId { get; set; }
        public int PaymentsMethodId { get; set; }
        public DateTime CreatedDate {  get; set; }

    }
}
