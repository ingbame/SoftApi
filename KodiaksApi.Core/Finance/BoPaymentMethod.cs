using KodiaksApi.Data.Finance;
using KodiaksApi.Entity.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Core.Finance
{
    public class BoPaymentMethod
    {
        #region Patron de Diseño
        private static BoPaymentMethod _instance;
        private static readonly object _instanceLock = new object();
        public static BoPaymentMethod Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new BoPaymentMethod();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<PaymentMethodEntity>> GetPaymentMethod(short? id = null)
        {
            var response = await DaPaymentMethod.Instance.GetPaymentMethod(id);
            return response;
        }
    }
}
