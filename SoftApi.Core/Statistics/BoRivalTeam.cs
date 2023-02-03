using SoftApi.Data.Statistics;
using SoftApi.Entity.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftApi.Core.Statistics
{
    public class BoRivalTeam
    {
        #region Patron de Diseño
        private static BoRivalTeam _instance;
        private static readonly object _instanceLock = new object();
        public static BoRivalTeam Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new BoRivalTeam();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<RivalTeamEntity>> GetPosition(int? id)
        {
            var response = await DaRivalTeam.Instance.GetPosition(id);
            return response;
        }
    }
}
