using MediatR;
using Social.Application.Models;

namespace Social.Application.Identities.Commands
{
    public class LoginUser : IRequest<OperationResult<string>>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
