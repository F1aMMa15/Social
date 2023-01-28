using Microsoft.EntityFrameworkCore;
using Social.Application.Abstractions;
using Social.Application.Enums;
using Social.Application.Posts.Commands.Interactions;
using Social.Dal;

namespace Social.Application.Posts.CommandHandlers.Interactions
{
    public class DeletePostInteractionHandler : RequestHandlerBase<DeletePostInteraction, bool>
    {
        public DeletePostInteractionHandler(DataContext dataContext)
            : base(dataContext) { }

        protected override async Task ExecuteRequestAsync(DeletePostInteraction request)
        {
            var post = await _dataContext.
                Posts.Include(p => p.Interactions).
                FirstOrDefaultAsync(p => p.Id == request.PostId);

            if (post == null)
            {
                _operationResult.SetError(ErrorCode.NotFound, $"Don't found post with id {request.PostId}");
                return;
            }

            var interaction = post.Interactions.FirstOrDefault(c => c.UserProfileId == request.UserId);
            if (interaction == null)
            {
                _operationResult.SetError(ErrorCode.NotFound, $"Don't found interaction with id");
                return;
            }

            post.RemoveInteraction(interaction);
            _dataContext.Posts.Update(post);
            await _dataContext.SaveChangesAsync();

            _operationResult.Payload = true;
        }
    }
}
