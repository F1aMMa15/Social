using Microsoft.EntityFrameworkCore;
using Social.Application.Abstractions;
using Social.Application.Enums;
using Social.Application.Posts.Commands.Comments;
using Social.Dal;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.Posts.CommandHandlers.Comments
{
    public class CreateCommentHandler : RequestHandlerBase<CreateComment, PostComment>
    {
        public CreateCommentHandler(DataContext dataContext)
            : base(dataContext) { }

        protected override async Task ExecuteRequestAsync(CreateComment request)
        {
            var post = await _dataContext.Posts.FirstOrDefaultAsync(p => p.Id == request.PostId);
            if (post == null)
            {
                _operationResult.SetError(ErrorCode.NotFound, $"Don't found post with id {request.PostId}");
                return;
            }

            var user = await _dataContext.UserProfiles.FirstOrDefaultAsync(up => up.Id == request.UserId);
            if (user == null)
            {
                _operationResult.SetError(ErrorCode.NotFound, $"Don't found user with id {request.UserId}");
                return;
            }

            var comment = PostComment.CreatePostComment(request.Text, post.Id, user.Id);

            post.AddComment(comment);
            _dataContext.Posts.Update(post);
            await _dataContext.SaveChangesAsync();

            _operationResult.Payload = comment;
        }
    }
}
