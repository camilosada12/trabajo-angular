using Business.Token;
using Data.Services;

namespace Web.Service_Extensions;

    public static class ServiceRepository
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<RolUserRepository>();
            services.AddScoped<RolFormPermissionRepository>();
            services.AddScoped<FormModuleRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<CrearToken>(); // Servicio extendido importante

            return services;
        }
    }

