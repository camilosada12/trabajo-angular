using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.relacionesModel
{
    public class RelacionRolUser : IEntityTypeConfiguration<RolUser>
    {
        public void Configure(EntityTypeBuilder<RolUser> builder)
        {
            builder.ToTable("roluser");

            builder.HasKey(ru => ru.id);

            builder.Property(ru => ru.isdeleted)
                   .IsRequired();

            // Relación RolUser -> Rol (muchos a uno)
            builder.HasOne(ru => ru.Rol)
                   .WithMany(r => r.RolUser)
                   .HasForeignKey(ru => ru.rolid)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_RolUser_Rol");

            // Relación RolUser -> User (muchos a uno)
            builder.HasOne(ru => ru.User)
                   .WithMany(u => u.Roles)
                   .HasForeignKey(ru => ru.userid)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_RolUser_User");
        }
    }
}
