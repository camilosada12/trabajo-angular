using Entity.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Business.Token;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[AllowAnonymous]

public class LoginController : ControllerBase
{
    private readonly CrearToken _token;
    private readonly ILogger<LoginController> _logger;

    public LoginController(CrearToken token, ILogger<LoginController> logger)
    {
        this._token = token;
        this._logger = logger;
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
}
       

