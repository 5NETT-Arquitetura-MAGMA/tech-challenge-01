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
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(c => c.Nome).HasMaxLength(60).IsRequired();
            builder.Property(c => c.Telefone).HasColumnType("INT").IsRequired();
            builder.Property(c => c.DDD).HasColumnType("INT").IsRequired();
            builder.Property(c => c.Email).HasMaxLength(50);
            builder.Property(c => c.Estado).HasMaxLength(2);
            builder.Property(c => c.Cidade).HasMaxLength(20);

        }
    }
}
