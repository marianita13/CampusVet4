using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class ClientRoleConfiguration : IEntityTypeConfiguration<ClientRol>
    {
        public void Configure(EntityTypeBuilder<ClientRol> builder)
        {
            builder.ToTable("ClientRole");

            builder.HasKey();
        }
    }
}