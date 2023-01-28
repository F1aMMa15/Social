using Microsoft.EntityFrameworkCore;
using Social.Application.Abstractions;
using Social.Application.Enums;
using Social.Application.Posts.Commands;
using Social.Dal;

namespace Social.Application.Posts.CommandHandlers
{
    public class DeletePostHandler : RequestHandlerBase<DeletePost, bool>
    {
        public DeletePostHandler(DataContext dataContext)
            : base(dataContext) { }

        protected override async Task ExecuteRequestAsync(DeletePost request)
        {
            var post = await _dataContext.Posts.FirstOrDefaultAsync(p => p.Id == request.Id);
            if (post == null)
            {
                _operationResult.SetError(ErrorCode.NotFound, $"Don't found post with id {request.Id}");
                return;
            }

            if (post.UserProfileId != request.UserId)
            {
                _operationResult.SetError(ErrorCode.Forbidden, $"You don't have access to update this post");
                return;
            }

            _dataContext.Posts.Remove(post);
            await _dataContext.SaveChangesAsync();
        }
    }
}
