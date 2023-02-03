using SoftApi.Core.Statistics;
using SoftApi.Data.Application;
using SoftApi.Data.Statistics;
using SoftApi.Entity.Application;
using SoftApi.Entity.Common;
using SoftApi.Entity.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftApi.Core.Application
{
    public class BoRole
    {
        #region Patron de Diseño
        private static BoRole _instance;
        private static readonly object _instanceLock = new object();
        public static BoRole Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new BoRole();
                    }
                }
                return _instance;
            }
        }
        #endregion
        #region Metodos publicos
        public async Task<ResponseEntity<List<RoleEntity>>> GetRole(short? id)
        {
            var response = new ResponseEntity<List<RoleEntity>>();
            try
            {
                response.Model = await DaRole.Instance.GetRole(id);
                return response;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
                if (ex.InnerException != null)
                    response.Message += $"\n{ex.InnerException.Message}";
                return response;
            }
        }
        #endregion
        #region Metodos privados
        #endregion
    }
}
