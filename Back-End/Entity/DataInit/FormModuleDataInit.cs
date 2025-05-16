using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace Entity.DataInit
{
    public static class FormModuleDataInit
    {
        public static void SeedFormModule(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FormModule>().HasData(
                new FormModule
                {
                    id = 1,
                    formid = 1,    // Asumiendo que el Form con id=1 existe
                    moduleid = 1,  // Asumiendo que el Module con id=1 existe
                    isdeleted = false
                },
                new FormModule
                {
                    id = 2,
                    formid = 2,
                    moduleid = 1,
                    isdeleted = false
                }
            );
        }
    }
}
