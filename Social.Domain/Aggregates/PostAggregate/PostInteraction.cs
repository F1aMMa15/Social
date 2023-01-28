namespace Social.Domain.Aggregates.PostAggregate
{
    public class PostInteraction
    {
        private PostInteraction() { }

        public Guid Id { get; private set; }
        public InteractionType InteractionType { get; private set; }

        public Guid PostId { get; private set; }
        public Guid UserProfileId { get; private set; }

        public static PostInteraction CreatePostInteraction(InteractionType type, Guid postId, Guid userProfileId)
        {
            return new PostInteraction
            {
                InteractionType = type,
                PostId = postId,
                UserProfileId = userProfileId
            };
        }
    }
}
