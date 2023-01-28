using MediatR;
using Social.Application.Models;

namespace Social.Application.Posts.Commands.Comments
{
    public class UpdateComment : IRequest<OperationResult<bool>>
    {
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; } = null!;
    }
}
