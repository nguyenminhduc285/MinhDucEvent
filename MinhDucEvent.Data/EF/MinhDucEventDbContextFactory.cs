using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MinhDucEvent.Data.EF
{
    public class MinhDucEventDbContextFactory : IDesignTimeDbContextFactory<MinhDucEventDbContext>
    {
        public MinhDucEventDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("MinhDucEventDb");

            var optionsBuilder = new DbContextOptionsBuilder<MinhDucEventDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new MinhDucEventDbContext(optionsBuilder.Options);
        }
    }
}