using System.ComponentModel.DataAnnotations;

namespace Social.Api.Contracts.Posts.Requets
{
    public class PostUpdate
    {
        [Required]
        [MinLength(3)]
        [MaxLength(500)]
        public string TextContent { get; set; } = null!;
    }
}
