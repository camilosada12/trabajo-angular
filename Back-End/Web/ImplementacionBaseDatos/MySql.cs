using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.InterfaceFactory;
using Entity.Context;
using Microsoft.EntityFrameworkCore;

namespace Web.ImplementacionBaseDatos
{
    public class MySql : InterfacesFactory
    {
        private readonly string _connectionString;

        public MySql(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
