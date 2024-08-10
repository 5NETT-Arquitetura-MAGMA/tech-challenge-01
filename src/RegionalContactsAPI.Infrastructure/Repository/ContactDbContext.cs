using Microsoft.EntityFrameworkCore;
using RegionalContactsAPI.Core.Entity;

namespace RegionalContactsAPI.Infrastructure.Repository
{
    public class ContactDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Cidade> Cidades { get; set; }

        public ContactDbContext(DbContextOptions<ContactDbContext> options)
            : base(options)
        {
        }

        //public ContactDbContext()
        //{
            
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContextOptions).Assembly);
        }

    }
}
