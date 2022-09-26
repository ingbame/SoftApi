using KodiaksApi.Data.Context;
using KodiaksApi.Data.DbModels;
using KodiaksApi.Entity.Application;
using KodiaksApi.Entity.Security;
using KodiaksApi.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Data
{
    public class DaSecurity
    {
        #region Patron de Diseño
        private static DaSecurity _instance;
        private static readonly object _instanceLock = new object();
        public static DaSecurity Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new DaSecurity();
                    }
                }
                return _instance;
            }
        }
        #endregion
        #region Metodos publicos        
        public CredentialsEntity GetUser(string user)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                var response = default(CredentialsEntity);
                var userResult = ctx.Users.Where(w => w.UserName.ToLower().Equals(user.ToLower())).FirstOrDefault();
                if (userResult != null)
                {
                    response = new CredentialsEntity();
                    response.User = userResult.CopyProperties(new UserEntity());
                    var getRole = ctx.Roles.Find(userResult.RoleId);
                    response.User.RoleEn = getRole.CopyProperties(new RoleEntity());
                    var getPerson = ctx.Members.Where(w => w.UserId == userResult.UserId).FirstOrDefault();
                    response.Member = getPerson.CopyProperties(new MemberEntity());
                }
                return response;
            }
        }        
        #endregion
        #region Metodos privados
        #endregion
    }
}
