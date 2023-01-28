using MediatR;
using Social.Application.Models;

namespace Social.Application.Identities.Commands
{
    public class RegisterUser : IRequest<OperationResult<string>>
    {
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string CurrentCity { get; set; } = null!;
    }
}
