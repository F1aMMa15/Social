using Microsoft.EntityFrameworkCore;
using Social.Application.Abstractions;
using Social.Application.Enums;
using Social.Application.Posts.Queries;
using Social.Dal;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.Posts.QueryHandlers
{
    public class GetPostByIdHandler : RequestHandlerBase<GetPostById, Post>
    {
        public GetPostByIdHandler(DataContext dataContext)
            : base(dataContext) { }

        protected override async Task ExecuteRequestAsync(GetPostById request)
        {
            var post = await _dataContext.Posts.FirstOrDefaultAsync(p => p.Id == request.PostId);
            if (post == null)
            {
                _operationResult.SetError(ErrorCode.NotFound, $"Don't found post with id {request.PostId}");
            }
        }
    }
}
