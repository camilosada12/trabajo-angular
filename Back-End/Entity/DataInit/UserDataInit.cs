using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace Entity.DataInit
{
    public static class UserDataInit
    {
        public static void SeetUser(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                 new User
                 {
                     id = 1,
                     username = "camilosada12",
                     email = "camilo@gmail.com",
                     password = "admin123",
                     active = true,
                     isdeleted = false,
                     personid = 1 // 👈 Relación con el Person con id 1
                 },
                 new User
                 {
                     id = 2,
                     username = "sara12312",
                     email = "sarita@gmail.com",
                     password = "sara12312",
                     active = true,
                     isdeleted = false,
                     personid = 2 // 👈 Relación con el Person con id 1
                 }
                );
        }
    }
}
