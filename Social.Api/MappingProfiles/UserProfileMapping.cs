using AutoMapper;
using Social.Api.Contracts.UserProfiles.Requets;
using Social.Api.Contracts.UserProfiles.Responses;
using Social.Application.UserProfiles.Commands;
using Social.Domain.Aggregates.UserProfileAggregate;

namespace Social.Api.MappingProfiles
{
    public class UserProfileMapping : Profile
    {
        public UserProfileMapping()
        {
            CreateMap<UserProfileCreateUpdate, UpdateUserProfile>();
            CreateMap<UserProfile, UserProfileResponse>();
            CreateMap<BasicInfo, BasicInfoResponse>();
        }
    }
}
