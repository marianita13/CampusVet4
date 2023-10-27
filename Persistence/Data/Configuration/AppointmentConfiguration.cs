using Domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("Appointment");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id);

        builder.Property(p => p.Date)
        .HasColumnType("date");

        builder.Property(p => p.Time)
        .HasColumnType("time");

        builder.HasOne(p => p.Clients)
        .WithMany(p => p.Appointments)
        .HasForeignKey(p => p.ClientId);

        builder.HasOne(p => p.Pets)
        .WithMany(p => p.Appointments)
        .HasForeignKey(p => p.PetId);

        builder.HasOne(p => p.Services)
        .WithMany(p => p.Appointments)
        .HasForeignKey(p => p.ServiceId);
    }
}

