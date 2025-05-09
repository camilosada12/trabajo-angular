using Business.Interfaces;
using Business.Services;
using Data.Interfaces;
using Data.Services;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Service_Extensions
{
    public static class ServicesCors
    {
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            var origenPermitido = configuration.GetValue<string>("Cors:OrigenPermitido");

            services.AddCors(options =>
            {
                options.AddPolicy("PoliticaCors", policy =>
                {
                    policy.WithOrigins(origenPermitido)
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            return services;
        }
    }
}
