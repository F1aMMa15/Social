using Social.Domain.Aggregates.UserProfileAggregate;

namespace Social.Absractions.Authentication
{
    public interface IAuthService
    {
        string GenerateJwt(UserProfile user);
    }
}
