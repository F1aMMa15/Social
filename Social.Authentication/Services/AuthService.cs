using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Social.Absractions.Authentication;
using Social.Authentication.Configuration;
using Social.Domain.Aggregates.UserProfileAggregate;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Social.Authentication.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtConfig _jwtConfig;

        public AuthService(IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig.Value;
        }

        public string GenerateJwt(UserProfile user)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var descriptor = new SecurityTokenDescriptor()
            {
                Audience = _jwtConfig.Audiences[0],
                Issuer = _jwtConfig.Issuer,
                Expires = DateTime.UtcNow.Add(_jwtConfig.ExpiryTime),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("IdentityId", user.IdentityId),
                    new Claim(ClaimTypes.NameIdentifier, user.IdentityId),
                    new Claim(JwtRegisteredClaimNames.Sub, user.BasicInfo.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.BasicInfo.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                            SecurityAlgorithms.HmacSha256Signature),
            };

            var token = jwtHandler.CreateToken(descriptor);
            return jwtHandler.WriteToken(token);
        }
    }
}
