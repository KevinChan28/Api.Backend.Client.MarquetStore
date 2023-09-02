using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Api.Client.MarquetStore.DTO
{
    public class UserRegister
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, PasswordPropertyText]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Telephone { get; set; }
    }
}
