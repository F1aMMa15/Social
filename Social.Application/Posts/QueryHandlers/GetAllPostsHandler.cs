using Microsoft.EntityFrameworkCore;
using Social.Application.Abstractions;
using Social.Application.Posts.Queries;
using Social.Dal;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.Posts.QueryHandlers
{
    public class GetAllPostsHandler : RequestHandlerBase<GetAllPosts, List<Post>>
    {
        public GetAllPostsHandler(DataContext dataContext)
            : base(dataContext) { }

        protected override async Task ExecuteRequestAsync(GetAllPosts request)
        {
            var posts = await _dataContext.Posts.ToListAsync();
            _operationResult.Payload = posts;
        }
    }
}
