using Entity.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Web.Custom
{
    public class utilidades
    {
        private readonly IConfiguration _configuration;

        public utilidades(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string EncriptarSHA256(string texto)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public string generarJWT(User modelo)
        {
            if (modelo == null)
            {
                throw new ArgumentNullException(nameof(modelo), "El modelo de usuario no puede ser nulo.");
            }

            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, modelo.id.ToString()),
                new Claim(ClaimTypes.Name, modelo.username!),
                new Claim(ClaimTypes.Email, modelo.email!),
                new Claim("active", modelo.active.ToString()),
                new Claim("isdeleted", modelo.isdeleted.ToString()),
                new Claim("personid", modelo.personid.ToString())
                };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwtConfig = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],  // Agregar Issuer
                audience: _configuration["JWT:Audience"],  // Agregar Audience
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(60),  // Establecer el tiempo de expiración
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }

    }
}
