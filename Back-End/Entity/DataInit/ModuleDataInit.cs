using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Entity.DataInit
{
    public static class ModuleDataInit
    {
        public static void SeedModule(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Module>().HasData(
                new Module
                {
                    id = 1,
                    name = "Módulo Administrativo",
                    description = "Módulo para administración general",
                    active = true,
                    isdeleted = false
                },
                new Module
                {
                    id = 2,
                    name = "Módulo de Reportes",
                    description = "Módulo encargado de generar reportes",
                    active = true,
                    isdeleted = false
                }
            );
        }
    }
}
