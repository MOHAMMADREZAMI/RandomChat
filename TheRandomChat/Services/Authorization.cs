using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TheRandomChat.Services
{
    public class Authorization
    {

        /// <summary>
        /// This Method For Create a jwt
        /// </summary>
        /// <param name="username"></param>
        /// <returns>return a JwtToken</returns>
        public string CreateJwt(string username)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT")));
            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, username),
            };

            JwtSecurityToken jwt = new JwtSecurityToken("RandomChat", audience: "RandomChat", signingCredentials: credentials, expires: DateTime.UtcNow.AddMinutes(1));

            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }



    }
}
