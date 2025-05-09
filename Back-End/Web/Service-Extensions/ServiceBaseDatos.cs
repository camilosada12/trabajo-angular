using Entity.Context;
using Microsoft.EntityFrameworkCore;

namespace Web.Service_Extensions;

    public static class ServiceBaseDatos
    {
        public static IServiceCollection AddDatabaseProvider(this IServiceCollection services, IConfiguration configuration)
        {
            string databaseProvider = configuration["DatabaseProvider"];
            string connectionString = configuration.GetConnectionString(databaseProvider);

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                switch (databaseProvider)
                {
                    case "SqlServer":
                        options.UseSqlServer(connectionString);
                        break;
                    case "PostgreSql":
                        options.UseNpgsql(connectionString);
                        break;
                    case "MySql":
                        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                        break;
                    default:
                        throw new InvalidOperationException("Proveedor de base de datos no soportado");
                }
            });

            return services;
        }
    }

