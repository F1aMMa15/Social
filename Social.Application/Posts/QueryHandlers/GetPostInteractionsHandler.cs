using Microsoft.EntityFrameworkCore;
using Social.Application.Abstractions;
using Social.Application.Enums;
using Social.Application.Posts.Queries;
using Social.Dal;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.Posts.QueryHandlers
{
    public class GetPostInteractionsHandler : RequestHandlerBase<GetPostInteractions, List<PostInteraction>>
    {
        public GetPostInteractionsHandler(DataContext dataContext)
            : base(dataContext) { }


        protected override async Task ExecuteRequestAsync(GetPostInteractions request)
        {
            var post = await _dataContext.Posts
                .Include(p => p.Interactions)
                .FirstOrDefaultAsync(p => p.Id == request.PostId);

            if (post == null)
            {
                _operationResult.SetError(ErrorCode.NotFound, $"Don't found post with id {request.PostId}");
                return;
            }

            _operationResult.Payload = post.Interactions.ToList();
        }
    }
}
