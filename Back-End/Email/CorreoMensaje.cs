using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace Email
{
    public class CorreoMensaje : IMensajeCorreo
    {
        private readonly IConfiguration _configuration;

        public CorreoMensaje(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task EnviarAsync(string destinatario, string asunto, string contenido)
        {
            var smtpConfig = _configuration.GetSection("SmtpSettings");

            var mensaje = new MailMessage
            {
                From = new MailAddress(smtpConfig["Email"]),
                Subject = asunto,
                Body = contenido,
                IsBodyHtml = true
            };

            mensaje.To.Add(destinatario);

            using var client = new SmtpClient
            {
                Host = smtpConfig["Host"],
                Port = int.Parse(smtpConfig["Port"]),
                EnableSsl = bool.Parse(smtpConfig["EnableSsl"]),
                Credentials = new NetworkCredential(smtpConfig["Email"], smtpConfig["Password"])
            };

            await client.SendMailAsync(mensaje);
        }
    }

}
