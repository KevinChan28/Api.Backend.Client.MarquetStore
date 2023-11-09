using System.ComponentModel.DataAnnotations;

namespace Api.Client.MarquetStore.DTO
{
    public class ChangePassword
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
