using System.ComponentModel.DataAnnotations;

namespace Social.Authentication.Contracts.Requests
{
    public class RegisterRequest
    {
        [Required]
        public string Password { get; set; } = null!;

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Phone { get; set; } = null!;

        [Required]
        public DateTime DateOfBirth { get; set; }

        public string? CurrentCity { get; set; }
    }
}
