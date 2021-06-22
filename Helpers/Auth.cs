using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace VotingSystemApi.Helpers
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
                    new Claim(ClaimTypes.NameIdentifier, id),
                    new Claim(ClaimTypes.Role, role.ToString()),
                    new Claim(ClaimTypes.Name, name),
                    new Claim("AcademicNum", academicNum),
                    //new Claim(ClaimTypes.NameIdentifier, name)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            string token = jwtTokenHandler.WriteToken(jwtToken);
            return token;
        }
    }
}
