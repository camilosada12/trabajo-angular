using Entity.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.FactoryDataBase;

namespace Web.Service_Extensions
{
    public static class ServiceDatabase
    {
        public static IServiceCollection AddDatabaseFactory(this IServiceCollection services, IConfiguration configuration)
        {
            var service = new SeleccionBaseDatos(configuration);
            var dbFactory = service.GetFactory();

            // Registras el ApplicationDbContext directamente
            services.AddScoped(provider => (ApplicationDbContext)dbFactory.CreateDbContext());

            return services;
        }
    }
}
