using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Business.Interfaces;
using Entity.DTOs;
using Entity.Model;
using Microsoft.AspNetCore.Mvc;
using Web.Custom;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IGenericService<UserDto> _userService;
        private readonly IGenericService<PersonDto> _personService;
        private readonly utilidades _utils;
        private readonly RolUserRepository _rolUserRepository;
        private readonly IConfiguration _config;

        public AuthController(IGenericService<UserDto> userService, IGenericService<PersonDto> personService, utilidades utils, RolUserRepository rolUserRepository, IConfiguration config)
        {
            _userService = userService;
            _personService = personService;
            _utils = utils;
            _rolUserRepository = rolUserRepository;
            _config = config; // Configuración para obtener el secreto del JWT
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        {
            // Crear nuevo UserDto
            var newUser = new UserDto
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                Password = registerDto.Password,
                PersonId = registerDto.PersonId
            };

            var createdUser = await _userService.AddAsync(newUser);

            if (createdUser == null)
            {
                return BadRequest("Error al registrar el usuario.");
            }

            // Asignar el rol
            await _rolUserRepository.AsignarRol(createdUser.id, registerDto.RolId);

            return Ok("Usuario registrado y rol asignado con éxito.");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            if (loginRequest == null || string.IsNullOrWhiteSpace(loginRequest.UserName) || string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                return BadRequest("Username and password are required.");
            }

            // Depuración: imprime los datos recibidos
            Console.WriteLine($"Login attempt: '{loginRequest.UserName}' / '{loginRequest.Password}'");

            // Obtener todos los usuarios de la base de datos
            var users = await _userService.GetAllAsync();

            // Mostrar los usuarios disponibles (para depuración)
            foreach (var u in users)
            {
                Console.WriteLine($"Checking -> UserName: '{u.UserName}', Password: '{u.Password}'");
            }

            // Buscar el usuario con coincidencia de nombre y contraseña (trim y sin distinguir mayúsculas en username)
            var authenticatedUser = users.FirstOrDefault(u =>
                u.UserName?.Trim().Equals(loginRequest.UserName.Trim(), StringComparison.OrdinalIgnoreCase) == true &&
                u.Password?.Trim() == loginRequest.Password.Trim());

            if (authenticatedUser == null)
            {
                Console.WriteLine("Login failed: Invalid credentials.");
                return Unauthorized("Invalid credentials");
            }

            // Obtener el rol del usuario
            var rol = await _rolUserRepository.GetRolNameByUserId(authenticatedUser.id);

            if (rol == null)
            {
                Console.WriteLine("Login failed: User role not found.");
                return Unauthorized("User role not found");
            }

            // Obtener la persona asociada
            var person = await _personService.GetByIdAsync(authenticatedUser.PersonId);

            // Generar el JWT
            var token = generarJWT(new User
            {
                id = authenticatedUser.id,
                username = authenticatedUser.UserName,
                email = authenticatedUser.Email,
                password = authenticatedUser.Password,
                personid = authenticatedUser.PersonId
            }, rol);

            return Ok(new { token, person });
        }

        private string generarJWT(User user, string rol)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
        new Claim(ClaimTypes.Name, user.username),
        new Claim(ClaimTypes.Role, rol)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],       // Añade el emisor
                audience: _config["JWT:Audience"],   // Añade la audiencia
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
