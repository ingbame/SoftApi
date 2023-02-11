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
    public class BoGamePlayedRivalDetail
    {
        #region Patron de Diseño
        private static BoGamePlayedRivalDetail _instance;
        private static readonly object _instanceLock = new object();
        public static BoGamePlayedRivalDetail Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new BoGamePlayedRivalDetail();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<GamePlayedRivalDetailEntity>> GetGamePlayedRivalDetail(int? id)
        {
            var response = await DaGamePlayedRivalDetail.Instance.GetGamePlayedRivalDetail(id);
            return response;
        }
        public async Task<List<GamePlayedRivalDetailEntity>> GetGamePlayedRivalDetailByGame(int? id)
        {
            var response = await DaGamePlayedRivalDetail.Instance.GetGamePlayedRivalDetailByGame(id);
            return response;
        }
        public async Task<GamePlayedRivalDetailEntity> NewGamePlayedRivalDetail(GamePlayedRivalDetailEntity request)
        {
            if (request == null)
                throw new Exception("No se ha ingresado información");

            isValidGamePlayedRivalDetailModel(request);
            var response = await DaGamePlayedRivalDetail.Instance.NewGamePlayedRivalDetail(request);
            return response;
        }

        public void isValidGamePlayedRivalDetailModel(GamePlayedRivalDetailEntity request)
        {
            if (!request.GameId.HasValue || request.GameId.Value <= 0)
                throw new Exception("Es necesario seleccionar un Juego para guardar.");

            if (!request.PositionAtBat.HasValue || request.PositionAtBat.Value <= 0)
                throw new Exception("Es necesario asignar una posición al bat.");

            if (!request.Inning.HasValue || request.Inning.Value <= 0)
                throw new Exception("Es necesario asignar una entrada.");

            if (!request.MemberId.HasValue || request.MemberId.Value <= 0)
                throw new Exception("Es necesario seleccionar un jugador.");

            if (request.Member != null || !request.Member.MemberId.HasValue || request.Member.MemberId.Value <= 0)
                throw new Exception("No se ha cargado la información del jugador.");

            if (!request.IsRun.HasValue)
                throw new Exception("Es necesario saber si es carrera anotada.");
            if (!request.IsHit.HasValue)
                throw new Exception("Es necesario saber si es hit anotada.");
            if (!request.IsDouble.HasValue)
                throw new Exception("Es necesario saber si es doblete anotada.");
            if (!request.IsTriple.HasValue)
                throw new Exception("Es necesario saber si es triplete anotada.");
            if (!request.IsHomeRun.HasValue)
                throw new Exception("Es necesario saber si es home run anotada.");

            if (!request.RunsBattedIn.HasValue || request.RunsBattedIn.Value <= 0)
                throw new Exception("Es necesario saber cuantas carreras ha impulsado.");
            if (!request.Walks.HasValue || request.Walks.Value <= 0)
                throw new Exception("Es necesario saber cuantas carreras ha impulsado.");
            if (!request.StrikeOut.HasValue || request.StrikeOut.Value <= 0)
                throw new Exception("Es necesario saber cuantas carreras ha impulsado.");
            if (!request.StolenBases.HasValue || request.StolenBases.Value <= 0)
                throw new Exception("Es necesario saber cuantas carreras ha impulsado.");
            if (!request.CaughtStealing.HasValue || request.CaughtStealing.Value <= 0)
                throw new Exception("Es necesario saber cuantas carreras ha impulsado.");

            if (!request.IsOut.HasValue)
                throw new Exception("Es necesario saber si es out.");

            if (!request.OutValue.HasValue || request.OutValue.Value <= 0)
                throw new Exception("Es necesario saber cuantas carreras ha impulsado.");
            if (!request.OutSector.HasValue || request.OutSector.Value <= 0)
                throw new Exception("Es necesario saber cuantas carreras ha impulsado.");

            if (!request.IsPitcher.HasValue)
                throw new Exception("Es necesario saber si el jugador es pitcher.");
        }
        public async Task<GamePlayedRivalDetailEntity> EditGamePlayedRivalDetail(long? id, GamePlayedRivalDetailEntity request)
        {
            if (!id.HasValue)
                throw new Exception("Debe de inluir id con la petición.");
            if (request == null)
                throw new Exception("No se ha ingresado información");
            if (id != request.RivalDetailId)
                throw new Exception("Debe de coincidir el id del Juego para editar con la petición.");
            isValidGamePlayedRivalDetailModel(request);
            var response = await DaGamePlayedRivalDetail.Instance.EditGamePlayedRivalDetail(request);
            return response;
        }
        public async Task<GamePlayedRivalDetailEntity> DeleteGamePlayedRivalDetail(GamePlayedRivalDetailEntity request)
        {
            if (request == null)
                throw new Exception("No se ha ingresado información");
            if (!request.RivalDetailId.HasValue || request.RivalDetailId <= 0)
                throw new Exception("Debe de tener un Id la entrada que desea manipular.");
            var response = await DaGamePlayedRivalDetail.Instance.DeleteGamePlayedRivalDetail(request.RivalDetailId);
            return response;
        }
    }
}
