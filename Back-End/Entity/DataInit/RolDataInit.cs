using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace Entity.DataInit
{
    public static class RolDataInit
    {
        public static void SeedRol(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<rol>().HasData(
                new rol
                {
                    id = 1,
                    name = "Administrador",
                    description = "Rol con todos los permisos",
                    isdeleted = false
                },
                new rol
                {
                    id = 2,
                    name = "Usuario",
                    description = "Rol estándar para usuarios normales",
                    isdeleted = false
                }
            );
        }
    }
}
