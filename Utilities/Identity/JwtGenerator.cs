using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utilities.DTOs;
using Utilities.Interfaces;

namespace Utilities
{
    public class JwtGenerator : IJwtGenerator
    {
        public string GenerateAccessToken(params Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(JwtTokenSettings.AccessExpiration));

            var accessToken = new JwtSecurityToken
                (
                    issuer: JwtTokenSettings.Issuer,
                    audience: JwtTokenSettings.Audience,
                    notBefore: DateTime.UtcNow,
                    expires: expires,
                    claims: claims,
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }
    }
}
