using KodiaksApi.Data;
using KodiaksApi.Entity.Application;
using KodiaksApi.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Core
{
    public class BoApplication
    {
        #region Patron de Diseño
        private static BoApplication _instance;
        private static readonly object _instanceLock = new object();
        public static BoApplication Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new BoApplication();
                    }
                }
                return _instance;
            }
        }
        #endregion
        #region Metodos publicos
        public ResponseEntity<List<MenuItemEntity>> GetMenu(string Role)
        {
            var response = new ResponseEntity<List<MenuItemEntity>>();
            try
            {
                response.Model = DaApplication.Instance.GetMenu(Role);
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
