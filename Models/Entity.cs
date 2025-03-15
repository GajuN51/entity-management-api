using System.ComponentModel.DataAnnotations;

namespace EntityAPI.Models
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();


        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Surname { get; set; }

        [Range(1, 120)]
        public required int Age { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        [Phone]
        public required string PhoneNumber { get; set; }
    }
}
