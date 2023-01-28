namespace Social.Api.Contracts.UserProfiles.Responses
{
    public record BasicInfoResponse
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime DateOfBirtg { get; set; }
        public string CurrentCity { get; set; } = null!;
    }
}
