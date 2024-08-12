namespace RegionalContactsTest
{
    public class DatabaseContext : IDisposable
    {
        public ContactDbContext DbContext { get; }

        public DatabaseContext()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.test.json")
                .Build();
            var options = new DbContextOptionsBuilder<ContactDbContext>()
                .UseSqlServer(config["ConnectionStrings:ContactDbContext"])
                .Options;

            DbContext = new ContactDbContext(options);
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}