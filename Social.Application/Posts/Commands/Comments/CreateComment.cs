using MediatR;
using Social.Application.Models;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.Posts.Commands.Comments
{
    public class CreateComment : IRequest<OperationResult<PostComment>>
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; } = null!;
    }
}
