using Microsoft.EntityFrameworkCore;
using Social.Application.Abstractions;
using Social.Application.Enums;
using Social.Application.Posts.Commands.Interactions;
using Social.Dal;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.Posts.CommandHandlers.Interactions
{
    public class CreatePostInteractionHandler : RequestHandlerBase<CreatePostInteraction, PostInteraction>
    {
        public CreatePostInteractionHandler(DataContext dataContext)
            : base(dataContext) { }

        protected override async Task ExecuteRequestAsync(CreatePostInteraction request)
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

            var interaction = PostInteraction.CreatePostInteraction(request.InteractionType, post.Id, user.Id);

            post.AddInteraction(interaction);
            _dataContext.Posts.Update(post);
            await _dataContext.SaveChangesAsync();

            _operationResult.Payload = interaction;
        }
    }
}
