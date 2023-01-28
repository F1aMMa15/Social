using MediatR;
using Social.Application.Models;

namespace Social.Application.Posts.Commands
{
    public class DeletePost : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
