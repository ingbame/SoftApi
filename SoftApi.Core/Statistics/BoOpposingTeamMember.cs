using SoftApi.Data.DbModels;
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
    public class BoOpposingTeamMember
    {
        #region Patron de Diseño
        private static BoOpposingTeamMember _instance;
        private static readonly object _instanceLock = new object();
        public static BoOpposingTeamMember Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new BoOpposingTeamMember();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<OpposingTeamMemberEntity>> GetTeamMember(int? id)
        {
            var response = await DaOpposingTeamMember.Instance.GetTeamMember(id);
            return response;
        }
        public async Task<OpposingTeamMemberEntity> NewTeamMember(OpposingTeamMemberEntity request)
        {
            if (request == null)
                throw new Exception("No se ha ingresado información");

            isValidTeamMemberModel(request);
            var response = await DaOpposingTeamMember.Instance.NewTeamMember(request);
            return response;
        }

        public void isValidTeamMemberModel(OpposingTeamMemberEntity request)
        {
            if (string.IsNullOrEmpty(request.MemberName?.Trim() ?? null))
                throw new Exception("Es necesario nombre del Equipo para guardar.");

            if (!request.IsPitcher.HasValue)
                throw new Exception("Debe de seleccionar si el es pitcher o no.");
        }
        public async Task<OpposingTeamMemberEntity> EditTeamMember(long? id, OpposingTeamMemberEntity request)
        {
            if (!id.HasValue)
                throw new Exception("Debe de inluir id con la petición.");
            if (request == null)
                throw new Exception("No se ha ingresado información");
            if (id != request.MemberId)
                throw new Exception("Debe de coincidir el id del Eqipo para editar con la petición.");
            isValidTeamMemberModel(request);
            var response = await DaOpposingTeamMember.Instance.EditTeamMember(request);
            return response;
        }
        public async Task<OpposingTeamMemberEntity> DeleteTeamMember(OpposingTeamMemberEntity request)
        {
            if (request == null)
                throw new Exception("No se ha ingresado información");
            if (!request.MemberId.HasValue || request.MemberId <= 0)
                throw new Exception("Debe de tener un Id la entrada que desea manipular.");
            var response = await DaOpposingTeamMember.Instance.DeleteTeamMember(request.MemberId);
            return response;
        }
    }
}
