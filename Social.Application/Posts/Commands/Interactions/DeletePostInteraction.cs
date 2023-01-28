using MediatR;
using Social.Application.Models;

namespace Social.Application.Posts.Commands.Interactions
{
    public class DeletePostInteraction : IRequest<OperationResult<bool>>
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
    }
}
