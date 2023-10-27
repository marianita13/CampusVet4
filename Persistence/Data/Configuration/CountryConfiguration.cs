using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class CountryConfiguracion : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Country");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(p => p.CountryName)
            .IsRequired()
            .HasMaxLength(50);
        }
    }
}