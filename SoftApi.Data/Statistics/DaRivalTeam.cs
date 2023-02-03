using SoftApi.Data.Context;
using SoftApi.Entity.Statistics;
using SoftApi.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftApi.Data.Statistics
{
    public class DaRivalTeam
    {
        #region Patron de Diseño Sigleton
        private static DaRivalTeam _instance;
        private static readonly object _instanceLock = new object();
        public static DaRivalTeam Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new DaRivalTeam();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<RivalTeamEntity>> GetPosition(int? id = null)
        {
            List<RivalTeamEntity> incomesLst = new List<RivalTeamEntity>();

            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                if (id.HasValue)
                {
                    var search = await ctx.RivalTeams.FindAsync(id);
                    incomesLst.Add(search.CopyProperties(new RivalTeamEntity()));
                }
                else
                    incomesLst = ctx.RivalTeams.Select(s => s.CopyProperties(new RivalTeamEntity())).ToList();
            }
            return incomesLst;
        }
    }
}
