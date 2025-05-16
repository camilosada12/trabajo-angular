using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace Entity.DataInit
{
    public static class RolUserDataInit
    {
        public static void SeedRolUser(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolUser>().HasData(
                new RolUser
                {
                    id = 1,
                    rolid = 1,    // Asumiendo que el rol con id=1 (Ej: Administrador) existe
                    userid = 1,   // Asumiendo que el usuario con id=1 (Ej: admin) existe
                    isdeleted = false
                },
                new RolUser
                {
                    id = 2,
                    rolid = 2,    // Rol Usuario
                    userid = 2,   // Usuario normal
                    isdeleted = false
                }
            );
        }
    }
}
