using Microsoft.EntityFrameworkCore;
using Social.Application.Abstractions;
using Social.Application.Enums;
using Social.Application.Identities.Commands;
using Social.Dal;

namespace Social.Application.Identities.CommandHandlers
{
    public class DeleteAccountHandler : RequestHandlerBase<DeleteAccount, bool>
    {
        public DeleteAccountHandler(DataContext dataContext)
            : base(dataContext) { }

        protected override async Task ExecuteRequestAsync(DeleteAccount request)
        {
            var user = await _dataContext.UserProfiles.FirstAsync(u => u.Id == request.UserId);
            if (user == null)
            {
                _operationResult.SetError(ErrorCode.NotFound, "The user don't dound");
                return;
            }

            var identity = await _dataContext.Users.FirstAsync(u => u.Id == user.IdentityId);
            if (identity == null)
            {
                _operationResult.SetError(ErrorCode.NotFound, "The user don't dound");
                return;
            }

            _dataContext.UserProfiles.Remove(user);
            _dataContext.Users.Remove(identity);
            await _dataContext.SaveChangesAsync();

            _operationResult.Payload = true;
        }
    }
}
