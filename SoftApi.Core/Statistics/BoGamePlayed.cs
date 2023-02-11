using SoftApi.Data.Finance;
using SoftApi.Data.Security;
using SoftApi.Data.Statistics;
using SoftApi.Entity.Finance;
using SoftApi.Entity.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftApi.Core.Statistics
{
    public class BoGamePlayed
    {
        #region Patron de Diseño
        private static BoGamePlayed _instance;
        private static readonly object _instanceLock = new object();
        public static BoGamePlayed Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new BoGamePlayed();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<GamePlayedEntity>> GetGamePlayed(int? id)
        {
            var response = await DaGamePlayed.Instance.GetGamePlayed(id);
            return response;
        }
        public async Task<GamePlayedEntity> NewGamePlayed(GamePlayedEntity request)
        {
            if (request == null)
                throw new Exception("No se ha ingresado información");

            isValidGamePlayedModel(request);
            var response = await DaGamePlayed.Instance.NewGamePlayed(request);
            return response;
        }

        public void isValidGamePlayedModel(GamePlayedEntity request)
        {
            if (!request.RivalTeamId.HasValue || request.RivalData.RivalTeamId.Value <= 0)
                throw new Exception("Es necesario seleccionar un Equipo para guardar.");

            if (request.RivalData == null || !request.RivalData.RivalTeamId.HasValue || request.RivalData.RivalTeamId.Value <= 0)
                throw new Exception("Es necesario la información del Equipo para guardar.");

            if (!request.WeWon.HasValue)
                throw new Exception("Es necesario saber si se ganó o se per´dió el juego.");
        }
        public async Task<GamePlayedEntity> EditGamePlayed(long? id, GamePlayedEntity request)
        {
            if (!id.HasValue)
                throw new Exception("Debe de inluir id con la petición.");
            if (request == null)
                throw new Exception("No se ha ingresado información");
            if (id != request.GameId)
                throw new Exception("Debe de coincidir el id del Juego para editar con la petición.");
            isValidGamePlayedModel(request);
            var response = await DaGamePlayed.Instance.EditGamePlayed(request);
            return response;
        }
        public async Task<GamePlayedEntity> DeleteGamePlayed(GamePlayedEntity request)
        {
            if (request == null)
                throw new Exception("No se ha ingresado información");
            if (!request.GameId.HasValue || request.GameId <= 0)
                throw new Exception("Debe de tener un Id la entrada que desea manipular.");
            var response = await DaGamePlayed.Instance.DeleteGamePlayed(request.GameId);
            return response;
        }
    }
}
