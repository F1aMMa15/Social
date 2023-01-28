using Social.Domain.Aggregates.PostAggregate;

namespace Social.Api.Contracts.Posts.Interactions.Responses
{
    public class InteractionResponse
    {
        public Guid Id { get; set; }
        public InteractionType InteractionType { get; set; }
        public Guid PostId { get; set; }
        public Guid UserProfileId { get; set; }
    }
}
