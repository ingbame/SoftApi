using KodiaksApi.Data.Finance;
using KodiaksApi.Data.Security;
using KodiaksApi.Data.Statistics;
using KodiaksApi.Entity.Finance;
using KodiaksApi.Entity.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Core.Statistics
{
    public class BoRoster
    {
        #region Patron de Diseño
        private static BoRoster _instance;
        private static readonly object _instanceLock = new object();
        public static BoRoster Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new BoRoster();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<RosterEntity>> GetRoster()
        {
            var response = await DaRoster.Instance.GetRoster();
            return response;
        }
        public async Task<RosterEntity> NewRoster(RosterEntity request)
        {
            if (request == null)
                throw new Exception("No se ha ingresado información");
            if (!string.IsNullOrEmpty(request.ByUser))
            {
                var credential = DaSecurity.Instance.GetUser(request.ByUser);
                if (credential != null)
                    request.CreatedBy = credential.User.UserId;
                else
                    throw new Exception("Usuario no existe.");
            }

            isValidRosterModel(request);
            var response = await DaRoster.Instance.NewRoster(request);
            return response;
        }
        public void isValidRosterModel(RosterEntity request)
        {
            if (!request.MemberId.HasValue || request.MemberId <= 0)
                throw new Exception("La persona que está capturando el movimiento no es válida, asegurese de tener permisos.");

            if (!request.PositionId.HasValue || request.PositionId <= 0)
                throw new Exception("No ha seleccionado un tipo de posición válido.");
            if (!request.CreatedBy.HasValue || request.CreatedBy <= 0)
                throw new Exception("No se puede guardar, si su sesion no está activa.");
        }
        public async Task<RosterEntity> DeleteRoster(RosterEntity request)
        {
            if (request == null)
                throw new Exception("No se ha ingresado información");
            if (!request.RosterId.HasValue || request.RosterId <= 0)
                throw new Exception("Debe de tener un Id la entrada que desea manipular.");
            var response = await DaRoster.Instance.DeleteRoster(request.RosterId);
            return response;
        }
    }
}
