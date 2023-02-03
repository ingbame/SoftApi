using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SoftApi.Data.Context
{
    public class DbContextConfig
    {
        public SoftDbContext CreateDbContext()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<SoftDbContext>();
            var connectionString = configuration.GetConnectionString("KodiaksDbConn");
            builder.UseSqlServer(connectionString);
            return new SoftDbContext(builder.Options);
        }
        public SoftDdExtentions ExtentionsDbContext()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<SoftDbContext>();
            var connectionString = configuration.GetConnectionString("KodiaksDbConn");
            builder.UseSqlServer(connectionString);
            return new SoftDdExtentions(builder.Options);
        }
    }
}
