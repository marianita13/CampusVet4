using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class PetConfiguracion : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("Pet");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(50);

            builder.Property(p => p.BirthDate)
            .HasColumnType("datetime");

            builder.HasOne(p => p.Breed)
            .WithMany(p => p.Pets)
            .HasForeignKey(p => p.BreedId);

            builder.HasOne(p => p.Clients)
            .WithMany(p => p.Pets)
            .HasForeignKey(p => p.ClientId);
        }
    }
}