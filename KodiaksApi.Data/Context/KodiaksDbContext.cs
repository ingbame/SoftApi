using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Data.Context
{
    public class KodiaksDbContext : DbContext
    {
        public KodiaksDbContext(DbContextOptions<KodiaksDbContext> options) : base(options)
        {

        }
    }
}
