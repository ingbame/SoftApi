using KodiaksApi.Data.Application;
using KodiaksApi.Data.Finance;
using KodiaksApi.Entity.Application;
using KodiaksApi.Entity.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Core.Application
{
    public class BoMember
    {
        #region Patron de Diseño
        private static BoMember _instance;
        private static readonly object _instanceLock = new object();
        public static BoMember Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new BoMember();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<MemberSelEntity>> GetMember(long? id = null)
        {
            var response = await DaMember.Instance.GetMember(id);
            return response;
        }
    }
}
