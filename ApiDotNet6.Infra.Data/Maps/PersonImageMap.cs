﻿using ApiDotNet6.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiDotNet6.Infra.Data.Maps
{
    public class PersonImageMap : IEntityTypeConfiguration<PersonImage>
    {
        public void Configure(EntityTypeBuilder<PersonImage> builder)
        {
            builder.ToTable("pessoaimagem");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnName("idimagem")
                   .UseIdentityColumn();

            builder.Property<int>(x => x.PersonId)
                   .HasColumnName("idpessoa");

            builder.Property(x => x.Description)
                   .HasColumnName("imgdescription");

            builder.Property(x => x.ImageUri)
                   .HasColumnName("imagemurl");

            builder.HasOne(x => x.Person)
                   .WithMany(x => x.PersonImages);

        }
    }
}
