using Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMensajeCorreo _mensaje;

        public EmailController(IMensajeCorreo mensaje)
        {
            _mensaje = mensaje;
        }

        [HttpPost("enviar-correo")]
        public async Task<IActionResult> EnviarCorreo([FromBody] string email)
        {
            await _mensaje.EnviarAsync(email, "buenas tardes", "que va a invitar caballero");
            return Ok("Correo enviado");
        }
    }
}
