using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace VotingSystemApi.Helper
{
    public class Auth
    {
        public string GenerateJSONWebToken(string userId, string userName)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Convert.FromBase64String(Startup.confg["Jwt:Key"]);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = null,              // Not required as no third-party is involved
                Audience = null,            // Not required as no third-party is involved
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(6),
                Subject = new ClaimsIdentity(new Claim[] 
                { 
                    new Claim("userid", userId),
                    new Claim(ClaimTypes.NameIdentifier, userName)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(jwtToken);
            return token;
        }
    }
}
