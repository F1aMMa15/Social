using System.ComponentModel.DataAnnotations;

namespace Social.Api.Contracts.Posts.Comments.Requests
{
    public class CommentCreate
    {
        [Required]
        [MinLength(3)]
        [MaxLength(500)]
        public string Text { get; set; } = null!;
    }
}
