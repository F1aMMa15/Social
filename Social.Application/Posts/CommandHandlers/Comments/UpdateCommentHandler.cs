using Microsoft.EntityFrameworkCore;
using Social.Application.Abstractions;
using Social.Application.Enums;
using Social.Application.Posts.Commands.Comments;
using Social.Dal;

namespace Social.Application.Posts.CommandHandlers.Comments
{
    public class UpdateCommentHandler : RequestHandlerBase<UpdateComment, bool>
    {
        public UpdateCommentHandler(DataContext dataContext)
            : base(dataContext) { }

        protected override async Task ExecuteRequestAsync(UpdateComment request)
        {
            var post = await _dataContext.
                Posts.Include(p => p.Comments).
                FirstOrDefaultAsync(p => p.Id == request.PostId);

            if (post == null)
            {
                _operationResult.SetError(ErrorCode.NotFound, $"Don't found post with id {request.PostId}");
                return;
            }

            var comment = post.Comments.FirstOrDefault(c => c.Id == request.CommentId);
            if (comment == null)
            {
                _operationResult.SetError(ErrorCode.NotFound, $"Don't found comment with id {request.CommentId}");
                return;
            }

            if (comment.UserProfileId != request.UserId)
            {
                _operationResult.SetError(ErrorCode.Forbidden, $"You cannot delete this comment");
                return;
            }

            comment.UpdateText(request.Text);
            _dataContext.Posts.Update(post);
            await _dataContext.SaveChangesAsync();

            _operationResult.Payload = true;
        }
    }
}
