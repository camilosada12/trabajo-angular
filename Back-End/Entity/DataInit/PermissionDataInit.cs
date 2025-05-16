using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace Entity.DataInit
{
    public static class PermissionDataInit
    {
        public static void SeedPermission(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>().HasData(
                new Permission
                {
                    id = 1,
                    name = "Leer",
                    description = "Permiso para lectura",
                    active = true,
                    isdeleted = false
                },
                new Permission
                {
                    id = 2,
                    name = "Escribir",
                    description = "Permiso para escritura",
                    active = true,
                    isdeleted = false
                }
            );
        }
    }
}
