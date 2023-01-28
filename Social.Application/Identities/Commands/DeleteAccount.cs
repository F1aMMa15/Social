using MediatR;
using Social.Application.Models;

namespace Social.Application.Identities.Commands
{
    public class DeleteAccount : IRequest<OperationResult<bool>>
    {
        public Guid UserId { get; set; }
    }
}
