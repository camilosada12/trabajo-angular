using System;
using Microsoft.EntityFrameworkCore;
using Entity.Model;

namespace Entity.DataInit
{
    /// <summary>
    /// Clase estática para inicializar datos semilla para la entidad RolFormPermission.
    /// </summary>
    public static class RolFormPermissionDataInit
    {
        /// <summary>
        /// Método de extensión para agregar datos iniciales (seed) para RolFormPermission.
        /// </summary>
        /// <param name="modelBuilder">El constructor del modelo para configuración.</param>
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
