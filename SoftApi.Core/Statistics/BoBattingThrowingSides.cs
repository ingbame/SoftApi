using SoftApi.Data;
using SoftApi.Data.Statistics;
using SoftApi.Entity.Application;
using SoftApi.Entity.Common;
using SoftApi.Entity.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftApi.Core.Statistics
{
    public class BoBattingThrowingSides
    {
        #region Patron de Diseño
        private static BoBattingThrowingSides _instance;
        private static readonly object _instanceLock = new object();
        public static BoBattingThrowingSides Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new BoBattingThrowingSides();
                    }
                }
                return _instance;
            }
        }
        #endregion
        #region Metodos publicos
        public async Task<ResponseEntity<List<BattingThrowingSideEntity>>> GetBattingThrowingSides(short? id)
        {
            var response = new ResponseEntity<List<BattingThrowingSideEntity>>();
            try
            {
                response.Model = await DaBattingThrowingSides.Instance.GetBattingThrowingSides(id);
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
