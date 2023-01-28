namespace Social.Api.Contracts.UserProfiles.Responses
{
    public record UserProfileResponse
    {
        public Guid Id { get; set; }
        public BasicInfoResponse BasicInfo { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
