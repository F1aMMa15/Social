using MediatR;
using Social.Application.Models;

namespace Social.Application.UserProfiles.Commands
{
    public class UpdateUserProfile : IRequest<OperationResult<bool>>
    {
        public Guid UserId { get; set; }
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string Phone { get; private set; } = null!;
        public DateTime DateOfBirtg { get; private set; }
        public string CurrentCity { get; private set; } = null!;
    }
}
