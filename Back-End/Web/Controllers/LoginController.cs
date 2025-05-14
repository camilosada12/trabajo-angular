using Entity.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Business.Token;
using Entity.Model;
using Google.Apis.Auth;


namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[AllowAnonymous]

public class LoginController : ControllerBase
{
    private readonly CrearToken _token;
    private readonly ILogger<LoginController> _logger;
    private readonly IConfiguration _configuration;
    private readonly UserRepository _user;

    public LoginController(CrearToken token, ILogger<LoginController> logger, IConfiguration configuration, UserRepository user)
    {
        this._token = token;
        this._logger = logger;
        _configuration = configuration;
        _user = user;
    }

    [HttpPost]

    public async Task<IActionResult> login([FromBody] LoginRequestDto dto)
    {
        try
        {
            var token = await _token.crearToken(dto);
            return StatusCode(StatusCodes.Status200OK, new { isSucces = true, token });
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "validacion fallida ,error al crear el token");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("google")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleTokenDto tokenDto)
    {
        try
        {
            // 1. validar el token con google
            var payload = await GoogleJsonWebSignature.ValidateAsync(tokenDto.Token, new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { _configuration["Google:ClientId"] }
            });

            // 2. buscar usuario en base de datos por email
            var user = await _user.getByEmail(payload.Email);

            // 3. si no existe, retornar error
            if (user == null)
            {
                return NotFound(new
                {
                    isSucces = false,
                    message = "el usuario no existe"
                });
            }

            // 4. crear dto para login
            var dto = new LoginRequestDto
            {
                UserName = user.username,
                Password = user.password
            };

            // 5. generar token personalizado
            var token = await _token.crearToken(dto);

            // 6. devolver token
            return Ok(new{ isSucces = true, token,});
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "error en login con google");
            return BadRequest(new
            {
                isSucces = false,
                message = ex.Message
            });
        }
    }

}


