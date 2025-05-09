using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Entity.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Business.Token;
public class CrearToken
{
    private readonly IConfiguration _configuration;
    private readonly UserRepository _user;

    public CrearToken(IConfiguration configuration, UserRepository user)
    {
        this._configuration = configuration;
        this._user = user;
    }

    public async Task<string> crearToken(LoginRequestDto dto)
    {
        var user = await _user.validacionUser(dto);
        if (user == null)
        {
            throw new UnauthorizedAccessException("credenciales incorrectas");
        }

        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.Name, user.password)  

            };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var jwtConfig = new JwtSecurityToken
        (
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:Expiration"])),
            signingCredentials: credentials
        );


        return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
    }
}

