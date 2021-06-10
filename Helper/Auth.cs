using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace VotingSystemApi.Helper
{
    public class Auth
    {
        public string GenerateJSONWebToken(string id, int role, string name, string academicNum)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Convert.FromBase64String(Startup.Configuration["Jwt:Key"]);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = null,              // Not required as no third-party is involved
                Audience = null,            // Not required as no third-party is involved
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(6),
                Subject = new ClaimsIdentity(new Claim[] 
                { 
                    new Claim("UserId", id),
                    new Claim("UserRole", role.ToString()),
                    new Claim("Name", name),
                    new Claim("AcademicNum", academicNum),
                    //new Claim(ClaimTypes.NameIdentifier, userName)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            string token = jwtTokenHandler.WriteToken(jwtToken);
            return token;
        }
    }
}
