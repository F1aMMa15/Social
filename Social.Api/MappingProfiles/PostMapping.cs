using AutoMapper;
using Social.Api.Contracts.Posts.Comments.Responses;
using Social.Api.Contracts.Posts.Interactions.Responses;
using Social.Api.Contracts.Posts.Requets;
using Social.Api.Contracts.Posts.Responses;
using Social.Application.Posts.Commands;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Api.MappingProfiles
{
    public class PostMapping : Profile
    {
        public PostMapping()
        {
            CreateMap<Post, PostResponse>();
            CreateMap<PostCreate, CreatePost>();
            CreateMap<PostUpdate, UpdatePost>();

            CreateMap<PostComment, CommentResponse>();

            CreateMap<PostInteraction, InteractionResponse>();
        }
    }
}
