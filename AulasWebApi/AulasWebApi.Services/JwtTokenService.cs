using AulasWebApi.Models;
using AulasWebApi.Services.Utils;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace AulasWebApi.Services
{
    public class JwtTokenService
    {
        private readonly JwtOptions _jwtOptions;
        public JwtTokenService(IOptions<JwtOptions> jwtOptions)
        {
            this._jwtOptions = jwtOptions.Value;
        }
        public string GenerateToken(User model, Person person)
        {
            // Gerar uma chave de acesso criptografada garantido que o usuario autenticou com sucesso
            // JWT - JSON Web Token
            // Claims

            List<Claim> claims = new List<Claim>
            {
                new Claim("FirstName", person.FirstName),
                new Claim("LastName", person.LastName),
                new Claim("Email", model.Email),
                new Claim("UserId", model.Id.ToString()),
                new Claim("PersonId", model.Person_Id.ToString()),
                new Claim("Role", "Admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._jwtOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.Now.AddHours(2);

            var token = new JwtSecurityToken(
                issuer: this._jwtOptions.Issuer,
                audience: this._jwtOptions.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenHandler;
        }
    }
}
