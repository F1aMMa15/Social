using AutoMapper;
using Social.Application.Identities.Commands;
using Social.Authentication.Contracts.Requests;

namespace Social.Api.MappingProfiles
{
    public class IdentityMapping : Profile
    {
        public IdentityMapping()
        {
            CreateMap<RegisterRequest, RegisterUser>();
            CreateMap<LoginRequest, LoginUser>();
        }
    }
}
