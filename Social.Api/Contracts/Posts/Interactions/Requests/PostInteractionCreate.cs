using Social.Domain.Aggregates.PostAggregate;
using System.ComponentModel.DataAnnotations;

namespace Social.Api.Contracts.Posts.Interactions.Requests
{
    public class PostInteractionCreate
    {
        [Required]
        public InteractionType InteractionType { get; set; }
    }
}
