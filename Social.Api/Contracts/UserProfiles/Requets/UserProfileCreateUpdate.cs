using System.ComponentModel.DataAnnotations;

namespace Social.Api.Contracts.UserProfiles.Requets
{
    public record UserProfileCreateUpdate
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [Phone]
        public string Phone { get; set; } = null!;

        [Required]
        public DateTime DateOfBirtg { get; set; }

        [Required]
        public string CurrentCity { get; set; } = null!;
    }
}
