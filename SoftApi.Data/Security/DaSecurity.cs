using SoftApi.Data.Context;
using SoftApi.Data.DbModels;
using SoftApi.Data.Finance;
using SoftApi.Entity.Application;
using SoftApi.Entity.Finance;
using SoftApi.Entity.Security;
using SoftApi.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftApi.Data.Security
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
                var userResult = ctx.Users.Where(w => w.UserName.ToLower().Equals(user.ToLower()) && w.IsVerified == true).FirstOrDefault();
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
        public async Task<bool> changePassword(UserEntity user)
        {
            var response = false;
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var usrToEdit = user.CopyProperties(new User());
                    ctx.Entry(usrToEdit).State = EntityState.Modified;

                    ctx.Entry(usrToEdit).Property(x => x.UserId).IsModified = false;
                    ctx.Entry(usrToEdit).Property(x => x.UserName).IsModified = false;
                    ctx.Entry(usrToEdit).Property(x => x.RoleId).IsModified = false;
                    ctx.Entry(usrToEdit).Property(x => x.CanEdit).IsModified = false;
                    ctx.Entry(usrToEdit).Property(x => x.SavePasswords).IsModified = false;
                    ctx.Entry(usrToEdit).Property(x => x.IsVerified).IsModified = false;
                    ctx.Entry(usrToEdit).Property(x => x.IsActive).IsModified = false;
                    ctx.Entry(usrToEdit).Property(x => x.CreatedDate).IsModified = false;

                    try
                    {
                        await ctx.SaveChangesAsync();
                        await trans.CommitAsync();
                        response = true;
                    }
                    catch (DbUpdateConcurrencyException) //Error al hacer el update en caso de que se elimine por otro usuario en el proceso
                    {
                        if (!await ctx.Users.AnyAsync(e => e.UserId == user.UserId))
                            throw new Exception("No se pudo actualizar, el registro ya no existe.");
                        else
                            throw new Exception("No se pudo actualizar, revise la información.");
                    }
                    return response;
                }
            }
        }
        #endregion
        #region Metodos privados
        #endregion
    }
}
