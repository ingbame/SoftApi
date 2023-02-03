using SoftApi.Entity.Application;
using SoftApi.Entity.Finance;
using SoftApi.Entity.Statistics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftApi.Data.Context
{
    public class SoftDdExtentions : SoftDbContext
    {
        public SoftDdExtentions()
        {
        }
        public SoftDdExtentions(DbContextOptions<SoftDbContext> options)
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
