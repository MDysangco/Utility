
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Utils
{
    public static class JwtFactory
    {
        public static string CreateUserToken(IConfiguration config, string subject, int minutes)
        {
            return CreateToken(config, subject, "User", minutes);
        }

        public static string CreateInternalServiceToken(IConfiguration config, string subject, int minutes)
        {
            return CreateToken(config, subject, "InternalService", minutes);
        }

        private static string CreateToken(IConfiguration config, string subject, string role , int minutes)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? ""));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, subject),
                new Claim("role", role)
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(minutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
