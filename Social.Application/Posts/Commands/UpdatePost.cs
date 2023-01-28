using MediatR;
using Social.Application.Models;

namespace Social.Application.Posts.Commands
{
    public class UpdatePost : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }
        public string TextContent { get; set; } = null!;
        public Guid UserId { get; set; }
    }
}
