namespace Social.Api.Contracts.Posts.Responses
{
    public record PostResponse
    {
        public Guid Id { get; set; }
        public string TextContent { get; set; } = null!;
        public Guid UserProfileId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
