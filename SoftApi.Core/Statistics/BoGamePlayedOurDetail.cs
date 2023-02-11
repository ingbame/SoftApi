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
    public class BoGamePlayedOurDetail
    {
        #region Patron de Diseño
        private static BoGamePlayedOurDetail _instance;
        private static readonly object _instanceLock = new object();
        public static BoGamePlayedOurDetail Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new BoGamePlayedOurDetail();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<GamePlayedOurDetailEntity>> GetGamePlayedOurDetail(int? id)
        {
            var response = await DaGamePlayedOurDetail.Instance.GetGamePlayedOurDetail(id);
            return response;
        }
        public async Task<List<GamePlayedOurDetailEntity>> GetGamePlayedOurDetailByGame(int? id)
        {
            var response = await DaGamePlayedOurDetail.Instance.GetGamePlayedOurDetailByGame(id);
            return response;
        }
        public async Task<GamePlayedOurDetailEntity> NewGamePlayedOurDetail(GamePlayedOurDetailEntity request)
        {
            if (request == null)
                throw new Exception("No se ha ingresado información");

            isValidGamePlayedOurDetailModel(request);
            var response = await DaGamePlayedOurDetail.Instance.NewGamePlayedOurDetail(request);
            return response;
        }

        public void isValidGamePlayedOurDetailModel(GamePlayedOurDetailEntity request)
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
        public async Task<GamePlayedOurDetailEntity> EditGamePlayedOurDetail(long? id, GamePlayedOurDetailEntity request)
        {
            if (!id.HasValue)
                throw new Exception("Debe de inluir id con la petición.");
            if (request == null)
                throw new Exception("No se ha ingresado información");
            if (id != request.OurDetailId)
                throw new Exception("Debe de coincidir el id del Juego para editar con la petición.");
            isValidGamePlayedOurDetailModel(request);
            var response = await DaGamePlayedOurDetail.Instance.EditGamePlayedOurDetail(request);
            return response;
        }
        public async Task<GamePlayedOurDetailEntity> DeleteGamePlayedOurDetail(GamePlayedOurDetailEntity request)
        {
            if (request == null)
                throw new Exception("No se ha ingresado información");
            if (!request.OurDetailId.HasValue || request.OurDetailId <= 0)
                throw new Exception("Debe de tener un Id la entrada que desea manipular.");
            var response = await DaGamePlayedOurDetail.Instance.DeleteGamePlayedOurDetail(request.OurDetailId);
            return response;
        }
    }
}
