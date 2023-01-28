namespace Social.Authentication.Configuration
{
    public class JwtConfig
    {
        public string Secret { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string[] Audiences { get; set; } = null!;
        public TimeSpan ExpiryTime { get; set; }
    }
}
