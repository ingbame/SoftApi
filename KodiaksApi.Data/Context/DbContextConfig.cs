using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KodiaksApi.Data.Context
{
    public class DbContextConfig
    {
        public KodiaksDbContext CreateDbContext()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<KodiaksDbContext>();
            var connectionString = configuration.GetConnectionString("KodiaksDbConn");
            builder.UseSqlServer(connectionString);
            return new KodiaksDbContext(builder.Options);
        }
    }
}
