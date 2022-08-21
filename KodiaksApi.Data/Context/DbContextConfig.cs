using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var connectionString = configuration.GetConnectionString("MuayThaiConn");
            builder.UseSqlServer(connectionString);
            return new KodiaksDbContext(builder.Options);
        }
    }
}
