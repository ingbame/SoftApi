using KodiaksApi.Data.Application;
using KodiaksApi.Data.Context;
using KodiaksApi.Entity.Application;
using KodiaksApi.Entity.Statistics;
using KodiaksApi.Util;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Data.Statistics
{
    public class DaBattingThrowingSides
    {
        #region Patron de Diseño
        private static DaBattingThrowingSides _instance;
        private static readonly object _instanceLock = new object();
        public static DaBattingThrowingSides Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new DaBattingThrowingSides();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<BattingThrowingSideEntity>> GetBattingThrowingSides(short? id = null)
        {
            List<BattingThrowingSideEntity> incomesLst = new List<BattingThrowingSideEntity>();

            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                if (id.HasValue)
                {
                    var search = await ctx.BattingThrowingSides.FindAsync(id);
                    incomesLst.Add(search.CopyProperties(new BattingThrowingSideEntity()));
                }
                else
                    incomesLst = ctx.BattingThrowingSides.Select(s => s.CopyProperties(new BattingThrowingSideEntity())).ToList();
            }
            return incomesLst;
        }
    }
}
