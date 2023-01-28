using MediatR;
using Social.Application.Models;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.Posts.Commands
{
    public class CreatePost : IRequest<OperationResult<Post>>
    {
        public string TextContent { get; set; } = null!;
        public Guid UserId { get; set; }
    }
}
