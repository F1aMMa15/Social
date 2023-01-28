using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Social.Api.Contracts.UserProfiles.Requets;
using Social.Api.Contracts.UserProfiles.Responses;
using Social.Api.Extensions;
using Social.Api.Filters;
using Social.Application.UserProfiles.Commands;
using Social.Application.UserProfiles.Queries;

namespace Social.Api.Controllers
{
    public class UserProfilesController : SocialControllerBase
    {
        public UserProfilesController(IMediator mediator, IMapper mapper)
            : base(mediator, mapper) { }

        [HttpGet]
        public async Task<IActionResult> GetAllUserProfiles()
        {
            var response = await _mediator.Send(new GetAllUserProfiles());
            if (response.IsError)
            {
                return HandleErrorResponse(response.Error);
            }

            var userProfilesResponse = _mapper.Map<List<UserProfileResponse>>(response.Payload);
            return Ok(userProfilesResponse);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserProfileById(string id)
        {
            var response = await _mediator.Send(new GetUserProfileById { Id = Guid.Parse(id) });
            if (response.IsError)
            {
                return HandleErrorResponse(response.Error);
            }

            var userProfileResponse = _mapper.Map<UserProfileResponse>(response.Payload);
            return Ok(userProfileResponse);
        }

        [HttpPatch]
        [ValidateModel]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfileCreateUpdate userProfile)
        {
            var userId = HttpContext.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var request = _mapper.Map<UpdateUserProfile>(userProfile);
            request.UserId = userId.Value;

            var response = await _mediator.Send(request);
            if (response.IsError)
            {
                return HandleErrorResponse(response.Error);
            }

            return NoContent();
        }
    }
}
