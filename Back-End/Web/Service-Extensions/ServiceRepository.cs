using Business.Token;
using Data.Services;
using Email;

namespace Web.Service_Extensions
{
    public static class ServiceRepository
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<RolUserRepository>();
            services.AddScoped<RolFormPermissionRepository>();
            services.AddScoped<FormModuleRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<CrearToken>();

            services.AddScoped<IMensajeCorreo, CorreoMensaje>();
            services.AddScoped<IMensajeTelegram, MensajeTelegram>();// ✅ Esto es lo que faltaba

            return services;
        }
    }
}
