using ApiDotNet6.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Infra.Data.Maps
{
    public class PermissionMaps : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("permissao");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("idpermissao")
                .UseIdentityColumn();

            builder.Property(x => x.VisualName)
                   .HasColumnName("nomevisual");

            builder.Property(x => x.PermissionName)
                   .HasColumnName("nomepermissao");

            builder.HasMany(x => x.UserPermissions)
                   .WithOne(c => c.Permission)
                   .HasForeignKey(k => k.PermissionId);
        }
    }
}
