using Microsoft.AspNetCore.Identity;
using Social.Absractions.Authentication;
using Social.Application.Abstractions;
using Social.Application.Enums;
using Social.Application.Identities.Commands;
using Social.Application.Models;
using Social.Dal;
using Social.Domain.Aggregates.UserProfileAggregate;

namespace Social.Application.Identities.CommandHandlers
{
    public class RegisterUserHandler : RequestHandlerBase<RegisterUser, string>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthService _authService;

        public RegisterUserHandler(UserManager<IdentityUser> userManager,
            DataContext dataContext,
            IAuthService authService)
            : base(dataContext)
        {
            _userManager = userManager;
            _authService = authService;
        }

        protected override async Task ExecuteRequestAsync(RegisterUser request)
        {
            var identity = await _userManager.FindByEmailAsync(request.Email);
            if (identity != null)
            {
                _operationResult.SetError(ErrorCode.AlreadyRegistred, "The user's already registred");
                return;
            }

            using var transaction = await _dataContext.Database.BeginTransactionAsync();

            identity = new IdentityUser
            {
                Email = request.Email,
                PhoneNumber = request.Phone,
                UserName = request.Email
            };

            var creationResult = await _userManager.CreateAsync(identity, request.Password);
            if (!creationResult.Succeeded)
            {
                await transaction.RollbackAsync();
                _operationResult.SetError(ErrorCode.ServerError, "Some problems while registraiting the user");
                return;
            }

            try
            {
                var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName,
                        request.Email, request.Phone, request.DateOfBirth, request.CurrentCity);

                var user = UserProfile.CreateUserProfile(identity.Id, basicInfo);

                _dataContext.UserProfiles.Add(user);
                await _dataContext.SaveChangesAsync();
                await transaction.CommitAsync();

                _operationResult.Payload = _authService.GenerateJwt(user);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _operationResult.SetError(ErrorCode.ServerError, ex.Message);
                return;
            }
        }
    }
}
