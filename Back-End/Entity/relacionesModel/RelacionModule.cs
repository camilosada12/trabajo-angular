using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.relacionesModel
{
    public class RelacionModule : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.ToTable("module");

            builder.HasKey(m => m.id);

            builder.Property(m => m.name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(m => m.description)
                   .HasMaxLength(250);

            builder.Property(m => m.active)
                   .IsRequired();

            builder.Property(m => m.isdeleted)
                   .IsRequired();

            // Relación: Module -> FormModules (uno a muchos)
            builder.HasMany(m => m.FormModules)
                   .WithOne(fm => fm.Module)  // asumiendo que FormModule tiene propiedad Module
                   .HasForeignKey(fm => fm.moduleid)  // asumiendo fk
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_Module_FormModules");
        }
    }
}
