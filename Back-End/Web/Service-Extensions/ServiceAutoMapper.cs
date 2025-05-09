using Entity.DTOs;

namespace Web.Service_Extensions
{
    public static class ServiceAutoMapper
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile)); // Agrega la configuración de AutoMapper
            return services;
        }
    }
}
