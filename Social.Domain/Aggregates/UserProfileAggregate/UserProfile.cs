namespace Social.Domain.Aggregates.UserProfileAggregate
{
    public class UserProfile
    {
        private UserProfile() { }

        public Guid Id { get; private set; }
        public string IdentityId { get; private set; } = null!;
        public BasicInfo BasicInfo { get; private set; } = null!;
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModifiedDate { get; private set; }

        public static UserProfile CreateUserProfile(string identityId, BasicInfo basicInfo)
        {
            return new UserProfile
            {
                IdentityId = identityId,
                BasicInfo = basicInfo,
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
            };
        }

        public void UpdateBasicInfo(BasicInfo basicInfo)
        {
            BasicInfo = basicInfo;
            LastModifiedDate = DateTime.Now;
        }
    }
}
