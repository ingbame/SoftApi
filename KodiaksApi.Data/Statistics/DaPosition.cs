using KodiaksApi.Data.Context;
using KodiaksApi.Entity.Statistics;
using KodiaksApi.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Data.Statistics
{
    public class DaPosition
    {
        #region Patron de Diseño Sigleton
        private static DaPosition _instance;
        private static readonly object _instanceLock = new object();
        public static DaPosition Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new DaPosition();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<PositionEntity>> GetPosition(short? id = null)
        {
            List<PositionEntity> incomesLst = new List<PositionEntity>();

            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                if (id.HasValue)
                {
                    var search = await ctx.Positions.FindAsync(id);
                    incomesLst.Add(search.CopyProperties(new PositionEntity()));
                }
                else
                    incomesLst = ctx.Positions.Select(s => s.CopyProperties(new PositionEntity())).ToList();
            }
            return incomesLst;
        }
    }
}
