using KodiaksApi.Data.Statistics;
using KodiaksApi.Entity.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Core.Statistics
{
    public class BoPosition
    {
        #region Patron de Diseño
        private static BoPosition _instance;
        private static readonly object _instanceLock = new object();
        public static BoPosition Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new BoPosition();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<PositionEntity>> GetPosition(short? id)
        {
            var response = await DaPosition.Instance.GetPosition(id);
            return response;
        }
    }
}
