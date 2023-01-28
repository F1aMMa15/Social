using System.ComponentModel.DataAnnotations;

namespace Social.Api.Contracts.Posts.Requets
{
    public record PostCreate
    {
        [Required]
        [MinLength(3)]
        [MaxLength(500)]
        public string TextContent { get; set; } = null!;
    }
}
