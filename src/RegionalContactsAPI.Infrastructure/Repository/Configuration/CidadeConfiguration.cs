using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegionalContactsAPI.Core.Entity;

namespace RegionalContactsAPI.Infrastructure.Repository.Configuration
{
    public class CidadeConfiguration : IEntityTypeConfiguration<Cidade>
    {
        public void Configure(EntityTypeBuilder<Cidade> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(c => c.NomeCidade).HasMaxLength(60).IsRequired();
            builder.Property(c => c.Estado).HasMaxLength(2);
            builder.Property(c => c.DDD).HasMaxLength(2);

            // Add index
            builder.HasIndex(c => c.NomeCidade);
        }
    }
}
