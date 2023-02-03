using SoftApi.Data.Finance;
using SoftApi.Entity.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftApi.Core.Finance
{
    public class BoMovementType
    {
        #region Patron de Diseño
        private static BoMovementType _instance;
        private static readonly object _instanceLock = new object();
        public static BoMovementType Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new BoMovementType();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<MovementTypeEntity>> GetMovementType(short? id = null)
        {
            var response = await DaMovementType.Instance.GetMovementType(id);
            return response;
        }
    }
}
