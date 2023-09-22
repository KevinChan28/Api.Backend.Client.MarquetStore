using System.ComponentModel.DataAnnotations;

namespace Api.Client.MarquetStore.DTO
{
    public class PayRegister
    {
        [Required]
        public int SaleId { get; set; }
        [Required]
        public int PaymentsMethodId { get; set; }
    }
}
