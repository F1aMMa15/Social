using Microsoft.EntityFrameworkCore;
using Social.Application.Abstractions;
using Social.Application.Enums;
using Social.Application.UserProfiles.Queries;
using Social.Dal;
using Social.Domain.Aggregates.UserProfileAggregate;

namespace Social.Application.UserProfiles.QueryHandlers
{
    public class GetUserProfileByIdHandler : RequestHandlerBase<GetUserProfileById, UserProfile>
    {
        public GetUserProfileByIdHandler(DataContext dataContext)
            : base(dataContext) { }

        protected override async Task ExecuteRequestAsync(GetUserProfileById request)
        {
            var userProfile = await _dataContext.UserProfiles.FirstOrDefaultAsync(up => up.Id == request.Id);
            if (userProfile == null)
            {
                _operationResult.SetError(ErrorCode.NotFound, $"Don't fount user profile with id {request.Id}");
            }
        }
    }
}
