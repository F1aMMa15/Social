using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Social.Application.Enums;
using Social.Application.Models;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SocialControllerBase : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly IMapper _mapper;

        public SocialControllerBase(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        protected IActionResult HandleErrorResponse(Error? error)
        {
            switch (error?.ErrorCode)
            {
                case ErrorCode.AlreadyRegistred:
                case ErrorCode.IncorrectPassword:
                    return BadRequest(error);

                case ErrorCode.Forbidden:
                    return Forbid();

                case ErrorCode.NotFound:
                case ErrorCode.DoesntExist: 
                    return NotFound(error);

                case ErrorCode.ServerError:
                    return StatusCode(500, error);

                default : return BadRequest(error);
            }
        }
    }
}
