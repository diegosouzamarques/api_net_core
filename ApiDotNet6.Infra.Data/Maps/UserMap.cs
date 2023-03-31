using ApiDotNet6.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiDotNet6.Infra.Data.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("usuario");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("idusuario");

            builder.Property(x => x.Email)
                   .HasColumnName("email");

            builder.Property(x => x.Username)
                   .HasColumnName("nomeusuario");

            builder.Property(x => x.Password)
                   .HasColumnName("senha");

            builder.Property(x => x.PasswordHash)
                   .HasColumnType("bytea")
                   .HasColumnName("senhahash");

            builder.Property(x => x.PasswordSalt)
                   .HasColumnType("bytea")
                   .HasColumnName("senhasalt");


            builder.Property(x => x.RefreshToken)
                   .HasColumnName("refreshtoken");

            builder.Property(x => x.DateCreated)
                   .HasColumnType("timestamp")
                   .HasColumnName("datacriacao");

            builder.Property(x => x.TokenExpires)
                   .HasColumnType("timestamp")
                   .HasColumnName("tokenexpiracao");


            builder.HasMany(x => x.UserPermissions)
                   .WithOne(x => x.User)
                   .HasForeignKey(k => k.UserId);

        }
    }
}
