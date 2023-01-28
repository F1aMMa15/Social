using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Social.Api.Extensions;
using Social.Api.Filters;
using Social.Application.Identities.Commands;
using Social.Authentication.Contracts.Requests;
using Social.Authentication.Contracts.Responses;

namespace Social.Api.Controllers
{
    public class IdentityController : SocialControllerBase
    {
        public IdentityController(IMediator mediator, IMapper mapper)
            : base(mediator, mapper)
        {
        }

        [HttpPost]
        [Route(nameof(Register))]
        [ValidateModel]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerInfo)
        {
            var request = _mapper.Map<RegisterUser>(registerInfo);
            var result = await _mediator.Send(request);
            if (result.IsError)
            {
                return HandleErrorResponse(result.Error);
            }

            var authResult = new AuthenticationResult { Token = result.Payload };
            return Ok(authResult);
        }

        [HttpPost]
        [Route(nameof(Login))]
        [ValidateModel]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginInfo)
        {
            var request = _mapper.Map<LoginUser>(loginInfo);
            var result = await _mediator.Send(request);
            if (result.IsError)
            {
                return HandleErrorResponse(result.Error);
            }

            var authResult = new AuthenticationResult { Token = result.Payload };
            return Ok(authResult);
        }

        [HttpPost]
        [Route(nameof(DeleteAccount))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = HttpContext.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var request = new DeleteAccount { UserId = userId.Value };
            var result = await _mediator.Send(request);
            if (result.IsError)
            {
                HandleErrorResponse(result.Error);
            }

            return Ok(result.Payload);
        }
    }
}
