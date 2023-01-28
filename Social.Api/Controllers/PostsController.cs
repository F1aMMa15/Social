using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Social.Api.Contracts.Posts.Comments.Requests;
using Social.Api.Contracts.Posts.Comments.Responses;
using Social.Api.Contracts.Posts.Interactions.Requests;
using Social.Api.Contracts.Posts.Interactions.Responses;
using Social.Api.Contracts.Posts.Requets;
using Social.Api.Contracts.Posts.Responses;
using Social.Api.Extensions;
using Social.Api.Filters;
using Social.Application.Posts.Commands;
using Social.Application.Posts.Commands.Comments;
using Social.Application.Posts.Commands.Interactions;
using Social.Application.Posts.Queries;

namespace Social.Api.Controllers
{
    public class PostsController : SocialControllerBase
    {
        public PostsController(IMediator mediator, IMapper mapper)
            : base(mediator, mapper) { }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            Console.WriteLine(this.Request);
            Console.WriteLine(this.HttpContext);
            var response = await _mediator.Send(new GetAllPosts());
            if (response.IsError)
            {
                return HandleErrorResponse(response.Error);
            }

            var postsResponse = _mapper.Map<List<PostResponse>>(response.Payload);
            return Ok(postsResponse);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPostById(string id)
        {
            var postId = Guid.Parse(id);
            var response = await _mediator.Send(new GetPostById { PostId = postId });
            if (response.IsError)
            {
                return HandleErrorResponse(response.Error);
            }

            var postResponse = _mapper.Map<PostResponse>(response.Payload);
            return Ok(postResponse);
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreatePost([FromBody] PostCreate post)
        {
            var userId = HttpContext.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var request = _mapper.Map<CreatePost>(post);
            request.UserId = userId.Value;

            var response = await _mediator.Send(request);
            if (response.IsError)
            {
                return HandleErrorResponse(response.Error);
            }

            var postResponse = _mapper.Map<PostResponse>(response.Payload);
            return CreatedAtAction(nameof(GetPostById), new { response?.Payload?.Id }, postResponse);
        }

        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdatePost(string id, [FromBody] PostUpdate post)
        {
            var postId = Guid.Parse(id);
            var userId = HttpContext.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var request = _mapper.Map<UpdatePost>(post);
            request.Id = postId;
            request.UserId = userId.Value;

            var response = await _mediator.Send(request);
            if (response.IsError)
            {
                return HandleErrorResponse(response.Error);
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeletePost(string id)
        {
            var postId = Guid.Parse(id);
            var userId = HttpContext.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var request = new DeletePost
            {
                Id = postId,
                UserId = userId.Value
            };

            var response = await _mediator.Send(request);
            if (response.IsError)
            {
                return HandleErrorResponse(response.Error);
            }

            return NoContent();
        }

        [HttpGet]
        [Route("{postId}/comments")]
        public async Task<IActionResult> GetPostComments(string postId)
        {
            var postIdGuid = Guid.Parse(postId);

            var request = new GetPostComments() {
                PostId = postIdGuid,
            };
            var response = await _mediator.Send(request);
            if (response.IsError)
            {
                return HandleErrorResponse(response.Error);
            }

            var actionRespone = _mapper.Map<List<CommentResponse>>(response.Payload);

            return Ok(actionRespone);
        }

        [HttpPost]
        [Route("{postId}/comments")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ValidateModel]
        public async Task<IActionResult> AddComment(string postId, [FromBody] CommentCreate comment)
        {
            var postIdGuid = Guid.Parse(postId);

            var userId = HttpContext.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var request = new CreateComment()
            {
                PostId = postIdGuid,
                Text = comment.Text,
                UserId = userId.Value
            };

            var response = await _mediator.Send(request);
            if (response.IsError)
            {
                return HandleErrorResponse(response.Error);
            }

            var actionRespone = _mapper.Map<CommentResponse>(response.Payload);

            return Ok(actionRespone);
        }


        [HttpPut]
        [Route("{postId}/comments/{commentId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ValidateModel]
        public async Task<IActionResult> UpdateComment(string postId, string commentId, [FromBody] CommentUpdate comment)
        {
            var postIdGuid = Guid.Parse(postId);
            var commentIdGuid = Guid.Parse(commentId);

            var userId = HttpContext.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var request = new UpdateComment
            {
                CommentId = commentIdGuid,
                PostId = postIdGuid,
                UserId = userId.Value,
                Text = comment.Text,
            };

            var result = await _mediator.Send(request);
            if (result.IsError)
            {
                return HandleErrorResponse(result.Error);
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{postId}/comments/{commentId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteComment(string postId, string commentId)
        {
            var postIdGuid = Guid.Parse(postId);
            var commentIdGuid = Guid.Parse(commentId);

            var userId = HttpContext.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var request = new DeleteComment
            {
                CommentId = commentIdGuid,
                PostId = postIdGuid,
                UserId = userId.Value
            };

            var result = await _mediator.Send(request);
            if (result.IsError)
            {
                return HandleErrorResponse(result.Error);
            }

            return NoContent();
        }

        [HttpGet]
        [Route("{postId}/interactions")]
        public async Task<IActionResult> GetPostInteractions(string postId)
        {
            var postIdGuid = Guid.Parse(postId);

            var request = new GetPostInteractions()
            {
                PostId = postIdGuid
            };

            var result = await _mediator.Send(request);
            if (result.IsError)
            {
                return HandleErrorResponse(result.Error);
            }

            var response = _mapper.Map<List<InteractionResponse>>(result.Payload);

            return Ok(response);
        }

        [HttpPost]
        [Route("{postId}/interactions")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ValidateModel]
        public async Task<IActionResult> AddPostInteraction(string postId, [FromBody] PostInteractionCreate interaction)
        {
            var postIdGuid = Guid.Parse(postId);

            var userId = HttpContext.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var request = new CreatePostInteraction()
            {
                PostId = postIdGuid,
                InteractionType = interaction.InteractionType,
                UserId = userId.Value
            };

            var result = await _mediator.Send(request);
            if (result.IsError)
            {
                return HandleErrorResponse(result.Error);
            }

            var response = _mapper.Map<InteractionResponse>(result.Payload);

            return Ok(response);
        }

        [HttpDelete]
        [Route("{postId}/interactions/{interactionId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeletePostInteraction(string postId)
        {
            var postIdGuid = Guid.Parse(postId);

            var userId = HttpContext.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var request = new DeletePostInteraction
            {
                PostId = postIdGuid,
                UserId = userId.Value
            };

            var result = await _mediator.Send(request);
            if (result.IsError)
            {
                return HandleErrorResponse(result.Error);
            }

            return NoContent();
        }
    }
}
