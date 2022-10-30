using KodiaksApi.Entity.Application;
using KodiaksApi.Entity.Finance;
using KodiaksApi.Entity.Statistics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Data.Context
{
    public class KodiaksDdExtentions : KodiaksDbContext
    {
        public KodiaksDdExtentions()
        {
        }
        public KodiaksDdExtentions(DbContextOptions<KodiaksDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MovementSelEntity>(ent => { ent.HasNoKey(); });
            modelBuilder.Entity<MovementSelYearMonthEntity>(ent => { ent.HasNoKey(); });
            modelBuilder.Entity<MemberSelEntity>(ent => { ent.HasNoKey(); });
            modelBuilder.Entity<RosterEntity>(ent => { ent.HasNoKey(); });
        }
    }
}
