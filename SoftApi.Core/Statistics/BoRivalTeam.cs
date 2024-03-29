﻿using SoftApi.Data.Finance;
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
    public class BoRivalTeam
    {
        #region Patron de Diseño
        private static BoRivalTeam _instance;
        private static readonly object _instanceLock = new object();
        public static BoRivalTeam Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new BoRivalTeam();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<RivalTeamEntity>> GetRivalTeam(int? id)
        {
            var response = await DaRivalTeam.Instance.GetRivalTeam(id);
            return response;
        }
        public async Task<RivalTeamEntity> NewRivalTeam(RivalTeamEntity request)
        {
            if (request == null)
                throw new Exception("No se ha ingresado información");

            isValidRivalTeamModel(request);
            var response = await DaRivalTeam.Instance.NewRivalTeam(request);
            return response;
        }

        public void isValidRivalTeamModel(RivalTeamEntity request)
        {
            if (string.IsNullOrEmpty(request.TeamName?.Trim() ?? null))
                throw new Exception("Es necesario nombre del Equipo para guardar.");

            if (!request.IsActive.HasValue)
                throw new Exception("Debe de seleccionar si el equipo está activo.");
        }
        public async Task<RivalTeamEntity> EditRivalTeam(long? id, RivalTeamEntity request)
        {
            if (!id.HasValue)
                throw new Exception("Debe de inluir id con la petición.");
            if (request == null)
                throw new Exception("No se ha ingresado información");
            if (id != request.RivalTeamId)
                throw new Exception("Debe de coincidir el id del Eqipo para editar con la petición.");
            isValidRivalTeamModel(request);
            var response = await DaRivalTeam.Instance.EditRivalTeam(request);
            return response;
        }
        public async Task<RivalTeamEntity> DeleteRivalTeam(RivalTeamEntity request)
        {
            if (request == null)
                throw new Exception("No se ha ingresado información");
            if (!request.RivalTeamId.HasValue || request.RivalTeamId <= 0)
                throw new Exception("Debe de tener un Id la entrada que desea manipular.");
            var response = await DaRivalTeam.Instance.DeleteRivalTeam(request.RivalTeamId);
            return response;
        }
    }
}
