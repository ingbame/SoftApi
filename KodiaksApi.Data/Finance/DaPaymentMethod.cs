using KodiaksApi.Data.Context;
using KodiaksApi.Entity.Finance;
using KodiaksApi.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Data.Finance
{
    public class DaPaymentMethod
    {
        #region Patron de Diseño Sigleton
        private static DaPaymentMethod _instance;
        private static readonly object _instanceLock = new object();
        public static DaPaymentMethod Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new DaPaymentMethod();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<PaymentMethodEntity>> GetPaymentMethod(short? id = null)
        {
            List<PaymentMethodEntity> types = new List<PaymentMethodEntity>();

            using (var ctx = new DbContextConfig().ExtentionsDbContext())
            {
                if (id.HasValue)
                {
                    var search = await ctx.PaymentMethods.FindAsync(id);
                    types.Add(search.CopyProperties(new PaymentMethodEntity()));
                }
                else
                    types = ctx.PaymentMethods.Select(s => s.CopyProperties(new PaymentMethodEntity())).ToList();
            }
            return types;
        }
    }
}
