using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace Entity.DataInit
{
    public static class RolFormPermissionDataInit
    {
        public static void SeedRolFormPermission(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolFormPermission>().HasData(
                new RolFormPermission
                {
                    id = 1,
                    rolid = 1,         // Ejemplo: rol Administrador
                    formid = 1,        // Form con id 1
                    permissionid = 1,  // Permiso con id 1
                    isdeleted = false
                },
                new RolFormPermission
                {
                    id = 2,
                    rolid = 2,         // Ejemplo: rol Usuario
                    formid = 1,
                    permissionid = 2,  // Otro permiso
                    isdeleted = false
                }
            );
        }
    }
}
