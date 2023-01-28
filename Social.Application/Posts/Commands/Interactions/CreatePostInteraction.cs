using MediatR;
using Social.Application.Models;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.Posts.Commands.Interactions
{
    public class CreatePostInteraction : IRequest<OperationResult<PostInteraction>>
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public InteractionType InteractionType { get; set; }
    }
}
