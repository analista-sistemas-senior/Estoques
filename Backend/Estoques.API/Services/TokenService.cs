using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Estoques.Service.DTOs.Usuario;
using Microsoft.IdentityModel.Tokens;

namespace Estoques.API.Services
{
    public class TokenService(IConfiguration configuration) : ITokenService
    {
        private readonly IConfiguration _configuration = configuration;

        public string GerarToken(UsuarioSaidaDTO usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var chave = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([ new Claim(ClaimTypes.NameIdentifier, usuario.IDUsuario.ToString()) ]),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JwtSettings:ExpiracaoTempo"]!)),
                Issuer = _configuration["JwtSettings:Emissor"],
                Audience = _configuration["JwtSettings:Audiencia"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(chave),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}