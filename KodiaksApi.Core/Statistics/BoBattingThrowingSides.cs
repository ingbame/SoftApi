using KodiaksApi.Data;
using KodiaksApi.Data.Statistics;
using KodiaksApi.Entity.Application;
using KodiaksApi.Entity.Common;
using KodiaksApi.Entity.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Core.Statistics
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
