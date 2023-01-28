using Microsoft.EntityFrameworkCore;
using Social.Application.Abstractions;
using Social.Application.Enums;
using Social.Application.Posts.Commands;
using Social.Dal;

namespace Social.Application.Posts.CommandHandlers
{
    public class UpdatePostHandler : RequestHandlerBase<UpdatePost, bool>
    {
        public UpdatePostHandler(DataContext dataContext)
            : base(dataContext) { }

        protected override async Task ExecuteRequestAsync(UpdatePost request)
        {
            var post = await _dataContext.Posts.FirstOrDefaultAsync(p => p.Id == request.Id);
            if (post == null)
            {
                _operationResult.SetError(ErrorCode.NotFound, $"Didn't found post with id {request.Id}");
                return;
            }

            if (post.UserProfileId != request.UserId)
            {
                _operationResult.SetError(ErrorCode.Forbidden, $"You don't have access to update this post");
                return;
            }

            post.UpdateTextContent(request.TextContent);

            _dataContext.Posts.Update(post);
            await _dataContext.SaveChangesAsync();
        }
    }
}
