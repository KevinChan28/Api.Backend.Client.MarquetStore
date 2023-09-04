using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api.Client.MarquetStore.DTO
{
    public class Login
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required,PasswordPropertyText]
        public string Password { get; set; }
    }
}
