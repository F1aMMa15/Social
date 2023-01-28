using Microsoft.EntityFrameworkCore;
using Social.Application.Abstractions;
using Social.Application.UserProfiles.Queries;
using Social.Dal;
using Social.Domain.Aggregates.UserProfileAggregate;

namespace Social.Application.UserProfiles.QueryHandlers
{
    public class GetAllUserProfilesHandler : RequestHandlerBase<GetAllUserProfiles, List<UserProfile>>
    {
        public GetAllUserProfilesHandler(DataContext dataContext)
            : base(dataContext) { }

        protected override async Task ExecuteRequestAsync(GetAllUserProfiles request)
        {
            var userProfiles = await _dataContext.UserProfiles.ToListAsync();
            _operationResult.Payload = userProfiles;
        }
    }
}
