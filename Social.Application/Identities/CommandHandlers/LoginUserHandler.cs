using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Social.Absractions.Authentication;
using Social.Application.Abstractions;
using Social.Application.Enums;
using Social.Application.Identities.Commands;
using Social.Dal;

namespace Social.Application.Identities.CommandHandlers
{
    public class LoginUserHandler : RequestHandlerBase<LoginUser, string>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthService _authService;

        public LoginUserHandler(UserManager<IdentityUser> userManager,
            IAuthService authService,
            DataContext dataContext)
            : base(dataContext)
        {
            _userManager = userManager;
            _authService = authService;
        }

        protected override async Task ExecuteRequestAsync(LoginUser request)
        {

            var identity = await _userManager.FindByEmailAsync(request.Email);
            if (identity == null)
            {
                _operationResult.SetError(ErrorCode.NotFound, "The user don't dound");
                return;
            }

            var chekPass = await _userManager.CheckPasswordAsync(identity, request.Password);
            if (!chekPass)
            {
                _operationResult.SetError(ErrorCode.IncorrectPassword, "Incorrect password");
                return;
            }

            var user = await _dataContext.UserProfiles.FirstAsync(u => u.IdentityId == identity.Id);
            _operationResult.Payload = _authService.GenerateJwt(user);
        }
    }
}
