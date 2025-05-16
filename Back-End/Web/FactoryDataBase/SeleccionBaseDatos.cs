using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Web.ImplementacionBaseDatos;
using Web.InterfaceFactory;

namespace Web.FactoryDataBase
{
    public class SeleccionBaseDatos
    {
        private readonly IConfiguration _configuration;

        public SeleccionBaseDatos(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public InterfacesFactory GetFactory()
        {
            var provider = _configuration["DatabaseProvider"];
            var connectionString = _configuration.GetConnectionString(provider);

            return provider switch
            {
                "SqlServer" => new SqlServer(connectionString),
                "PostgreSql" => new PostgreSql(connectionString),
                "MySql" => new MySql(connectionString),
                _ => throw new InvalidOperationException("Proveedor de base de datos no soportado")
            };
        }
    }
}
