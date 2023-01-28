namespace Social.Domain.Aggregates.PostAggregate
{
    public class PostComment
    {
        private PostComment() { }

        public Guid Id { get; private set; }
        public string Text { get; private set; } = null!;
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModifiedDate { get; private set; }

        public Guid PostId { get; private set; }
        public Guid UserProfileId { get; private set; }

        public static PostComment CreatePostComment(string text, Guid postId, Guid userProfileId)
        {
            return new PostComment
            {
                Text = text,
                PostId = postId,
                UserProfileId = userProfileId,
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
            };
        }

        public void UpdateText(string text)
        {
            Text = text;
            LastModifiedDate = DateTime.Now;
        }
    }
}
