using Microsoft.EntityFrameworkCore;
using Social.Application.Abstractions;
using Social.Application.Enums;
using Social.Application.Posts.Commands;
using Social.Dal;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.Posts.CommandHandlers
{
    public class CreatePostHandler : RequestHandlerBase<CreatePost, Post>
    {
        public CreatePostHandler(DataContext dataContext)
            : base(dataContext) { }

        protected override async Task ExecuteRequestAsync(CreatePost request)
        {
            var user = await _dataContext.UserProfiles.FirstAsync(up => up.Id == request.UserId);
            if (user == null)
            {
                _operationResult.SetError(ErrorCode.NotFound, $"Don't found user with id {request.UserId}");
                return;
            }
            var post = Post.CreatePost(request.TextContent, user.Id);

            _dataContext.Posts.Add(post);
            await _dataContext.SaveChangesAsync();
        }
    }
}
