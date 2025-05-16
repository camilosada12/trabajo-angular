using Email.Interfaz;
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

            string asunto = "Notificación importante";
            string contenido = @"
        <html>
          <body style='font-family: Arial, sans-serif; background-color: #f9f9f9; padding: 20px;'>
            <div style='background-color: white; padding: 20px; border-radius: 5px; max-width: 600px; margin: auto;'>
              <h1 style='color: #007BFF;'>Hola, esto es un correo de prueba</h1>
              <p>Gracias por usar nuestro servicio. Este correo es para confirmarte que todo funciona correctamente.</p>
              <p style='font-size: 12px; color: gray; margin-top: 30px;'>© 2025 Tu Empresa. Todos los derechos reservados.</p>
            </div>
          </body>
        </html>";

            await _mensaje.EnviarAsync(email, asunto, contenido);
            return Ok("Correo enviado");
        }
    }
}
