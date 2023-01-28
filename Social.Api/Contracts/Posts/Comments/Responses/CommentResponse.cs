namespace Social.Api.Contracts.Posts.Comments.Responses
{
    public class CommentResponse
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = null!;
        public Guid PostId { get; set; }
        public Guid UserProfileId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
