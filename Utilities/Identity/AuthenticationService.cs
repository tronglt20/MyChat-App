using System.Security.Claims;
using Utilities.DTOs;
using Utilities.Interfaces;

namespace Utilities
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtGenerator _jwtGenerator;

        public AuthenticationService(IJwtGenerator jwtGenerator)
        {
            _jwtGenerator = jwtGenerator;
        }

        public Task<LoginResult> GetLoginResultAsync(string userId, string name, string email)
        {
            var claims = new List<Claim>
            {
                new Claim(AppClaimType.UserId, userId),
                new Claim(AppClaimType.UserName, name),
                new Claim(AppClaimType.UserEmail, email),
            };

            var accessToken =  _jwtGenerator.GenerateAccessToken(claims.ToArray());

            var result = new LoginResult
            {
                AccessToken = accessToken,
            };

            return Task.FromResult(result);
        }
    }

}
