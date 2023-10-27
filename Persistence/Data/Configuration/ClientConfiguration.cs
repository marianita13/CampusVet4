using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class ClientConfiguracion : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(p => p.ClientName)
            .IsRequired()
            .HasMaxLength(50);

            builder.Property(p => p.LastNames)
            .IsRequired()
            .HasMaxLength(50);

            builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(80);
        }
    }
}