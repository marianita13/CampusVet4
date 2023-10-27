using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class ClientAddressConfiguracion : IEntityTypeConfiguration<ClientAddress>
    {
        public void Configure(EntityTypeBuilder<ClientAddress> builder)
        {
            builder.ToTable("ClientAddress");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(p => p.TypeOfStreet)
            .IsRequired()
            .HasMaxLength(50);

            builder.Property(p => p.FirstNumber)
            .IsRequired()
            .HasColumnType("int");

            builder.Property(p => p.Letter)
            .HasMaxLength(1);

            builder.Property(p => p.Bis)
            .HasMaxLength(3);

            builder.Property(p => p.SecondLetter)
            .HasMaxLength(2);

            builder.Property(p => p.Cardinal)
            .HasMaxLength(10);

            builder.Property(p => p.SecondNumber)
            .IsRequired()
            .HasColumnType("int");

            builder.Property(p => p.ThirdLetter)
            .HasMaxLength(10);

            builder.Property(p => p.ThirdNumber)
            .IsRequired()
            .HasColumnType("int");

            builder.Property(p => p.SecondCardinal)
            .HasMaxLength(10);

            builder.Property(p => p.Complement)
            .HasMaxLength(50);

            builder.Property(p => p.ZipCode)
            .HasMaxLength(10);

            builder.HasOne(a => a.Clients)
            .WithOne(b => b.ClientAddress)
            .HasForeignKey<ClientAddress>(b => b.ClientId);

            builder.HasOne(p => p.Cities)
            .WithOne(p => p.ClientAddresses)
            .HasForeignKey<ClientAddress>(p => p.IdCity);
        }
    }
}