using SoftApi.Data;
using SoftApi.Data.Application;
using SoftApi.Data.Finance;
using SoftApi.Entity.Application;
using SoftApi.Entity.Common;
using SoftApi.Entity.Finance;
using SoftApi.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SoftApi.Core.Application
{
    public class BoMember
    {
        #region Design Pattern
        private static BoMember _instance;
        private static readonly object _instanceLock = new object();
        public static BoMember Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new BoMember();
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region Public Methods
        public async Task<List<MemberSelEntity>> GetMember(long? id = null)
        {
            var response = await DaMember.Instance.GetMember(id);
            return response;
        }
        public async Task<CredentialsDtoEntity> CreateMember(CredentialsEntity request)
        {
            if (request == null)
                throw new Exception("No se ha ingresado información");

            isValidMember(request, true);

            if (string.IsNullOrEmpty(request.Member.NickName?.Trim()))
                request.Member.NickName = request.User.UserName.Split(" ").FirstOrDefault();

            string newSalt = BoSecurity.Instance.GenerateSalt();
            byte[] hashedPassword = BoSecurity.Instance.GetHash(request.User.Password, newSalt);
            string hashedBase64StringPassword = Convert.ToBase64String(hashedPassword);

            var result = await DaMember.Instance.CreateMember(request, newSalt, hashedBase64StringPassword);
            return result;
        }
        public async Task<CredentialsDtoEntity> EditMember(long? id, CredentialsEntity request)
        {
            if (!id.HasValue)
                throw new Exception("Debe de inluir id con la petición.");
            if (request == null)
                throw new Exception("No se ha ingresado información");
            if (id != request.Member.MemberId)
                throw new Exception("Debe de coincidir el id del movimiento con la petición.");

            isValidMember(request);

            if (string.IsNullOrEmpty(request.Member.NickName?.Trim()))
                request.Member.NickName = request.User.UserName.Split(" ").FirstOrDefault();

            var result = await DaMember.Instance.EditMember(request);
            return result;
        }
        #endregion

        #region Private Methods
        private void isValidMember(CredentialsEntity request, bool isNew = false)
        {
            if (string.IsNullOrEmpty(request.Member.FullName?.Trim()))
                throw new Exception("Nombre de persona vacío.");

            if (request.Member.Birthday == null)
                throw new Exception("Revise su fecha de nacimiento.");

            if (request.Member.Birthday < DateTime.Parse("1940-01-01"))
                throw new Exception("Excede los 80 años, revise su fecha de nacimiento.");

            if (string.IsNullOrEmpty(request.Member.CellPhoneNumber?.Trim()))
                throw new Exception("Número de celular vacío.");

            if (string.IsNullOrEmpty(request.User.UserName?.Trim()))
                throw new Exception("Nombre de usuario vacío.");

            if (isNew)
            {
                if (string.IsNullOrEmpty(request.User.Password?.Trim()))
                    throw new Exception("Contraseña vacía.");
            }
        }
        #endregion
    }
}
