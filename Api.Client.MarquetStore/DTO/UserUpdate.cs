using System.ComponentModel.DataAnnotations;

namespace Api.Client.MarquetStore.DTO
{
    public class UserUpdate
    {
        [Required]
        public int Id { get; set; } 
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string? Telephone { get; set; }
    }
}
