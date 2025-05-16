using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.InterfaceFactory;
using Entity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Web.ImplementacionBaseDatos;

public class SqlServer : InterfacesFactory
{
    private readonly string _connectionString;

    public SqlServer(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(_connectionString);
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}



