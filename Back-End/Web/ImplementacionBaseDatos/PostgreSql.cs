using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.InterfaceFactory;
using Entity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Web.ImplementacionBaseDatos
{
    public class PostgreSql : InterfacesFactory
    {
        private readonly string _connectionString;

        public PostgreSql(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(_connectionString);
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
