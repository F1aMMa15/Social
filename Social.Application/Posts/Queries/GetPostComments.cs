﻿using MediatR;
using Social.Application.Models;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.Posts.Queries
{
    public class GetPostComments : IRequest<OperationResult<List<PostComment>>>
    {
        public Guid PostId { get; set; }
    }
}
