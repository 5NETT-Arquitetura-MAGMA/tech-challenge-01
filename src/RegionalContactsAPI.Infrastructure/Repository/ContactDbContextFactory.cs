using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace RegionalContactsAPI.Infrastructure.Repository
{
    public class ContactDbContextFactory : IDesignTimeDbContextFactory<ContactDbContext>
    {
        public ContactDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContactDbContext>();

            // Obtenha a configuração do appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../RegionalContactsAPI"))
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("ContactDbContext");

            optionsBuilder.UseSqlServer(connectionString);

            return new ContactDbContext(optionsBuilder.Options);
        }
    }
}
