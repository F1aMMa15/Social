using Microsoft.EntityFrameworkCore;
using Social.Application.Abstractions;
using Social.Application.Enums;
using Social.Application.UserProfiles.Commands;
using Social.Dal;
using Social.Domain.Aggregates.UserProfileAggregate;

namespace Social.Application.UserProfiles.CommandHandlers
{
    public class UpdateUserProfileHandler : RequestHandlerBase<UpdateUserProfile, bool>
    {
        public UpdateUserProfileHandler(DataContext dataContext)
            : base(dataContext) { }

        protected override async Task ExecuteRequestAsync(UpdateUserProfile request)
        {
            var userProfile = await _dataContext.UserProfiles.
                FirstOrDefaultAsync(x => x.Id == request.UserId);

            if (userProfile == null)
            {
                _operationResult.SetError(ErrorCode.NotFound, $"Don't fount user with id {request.UserId}");
                return;
            }

            var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName,
                request.Email, request.Phone, request.DateOfBirtg, request.CurrentCity);

            userProfile.UpdateBasicInfo(basicInfo);

            _dataContext.UserProfiles.Update(userProfile);
            await _dataContext.SaveChangesAsync();
        }
    }
}
