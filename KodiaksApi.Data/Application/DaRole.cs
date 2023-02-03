using SoftApi.Data.Context;
using SoftApi.Data.Statistics;
using SoftApi.Entity.Application;
using SoftApi.Entity.Statistics;
using SoftApi.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftApi.Data.Application
{
    public class DaRole
    {
        #region Patron de Diseño
        private static DaRole _instance;
        private static readonly object _instanceLock = new object();
        public static DaRole Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new DaRole();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<RoleEntity>> GetRole(short? id = null)
        {
            List<RoleEntity> incomesLst = new List<RoleEntity>();

            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                if (id.HasValue)
                {
                    var search = await ctx.Roles.FindAsync(id);
                    incomesLst.Add(search.CopyProperties(new RoleEntity()));
                }
                else
                    incomesLst = ctx.Roles.Select(s => s.CopyProperties(new RoleEntity())).ToList();
            }
            return incomesLst;
        }
    }
}
