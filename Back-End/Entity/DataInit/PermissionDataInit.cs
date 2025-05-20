using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace Entity.DataInit
{
    /// <summary>
    /// Clase estática para inicializar datos semilla (seed) de la entidad <see cref="Permission"/>.
    /// </summary>
    public static class PermissionDataInit
    {
        /// <summary>
        /// Método de extensión para agregar datos iniciales (seed) a la entidad <see cref="Permission"/>.
        /// </summary>
        /// <param name="modelBuilder">Instancia de <see cref="ModelBuilder"/> usada para configurar el modelo de datos.</param>
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
