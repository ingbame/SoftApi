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
                    response.User.Role = getRole.CopyProperties(new RoleEntity());
                    var getPerson = ctx.Members.Where(w => w.UserId == userResult.UserId).FirstOrDefault();
                    response.Member = getPerson.CopyProperties(new MemberEntity());
                }
                return response;
            }
        }
        public CredentialsDtoEntity CreateNewPerson(CredentialsEntity newUser, string salt, string password)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                var personaToAdd = newUser.Member.CopyProperties(new Member());
                var getDefaultRole = ctx.Roles.Where(w => w.RoleDescription.Equals("Usuario")).FirstOrDefault();
                var userToAdd = new User
                {
                    UserName = newUser.User.UserName,
                    Password = password,
                    PasswordSalt = salt,
                    RoleId = getDefaultRole.RoleId
                };

                var trans = ctx.Database.BeginTransaction();
                var userResult = ctx.Users.Where(w => w.UserName == userToAdd.UserName).FirstOrDefault();
                if (userResult != null)
                    throw new Exception("Nombre de usuario registrado ya existe.");
                var addUser = ctx.Users.Add(userToAdd);
                if (addUser.State != EntityState.Added)
                    throw new Exception("No se agregó el usuario correctamente");
                ctx.SaveChanges();

                //Validar que la persona exista, pendiente
                personaToAdd.UserId = userToAdd.UserId;
                var addPersona = ctx.Members.Add(personaToAdd);
                if (addPersona.State != EntityState.Added)
                    throw new Exception("No se agregó el afiliado correctamente");
                ctx.SaveChanges();

                trans.Commit();

                var response = new CredentialsDtoEntity
                {
                    Member = personaToAdd.CopyProperties(new MemberEntity()),
                    User = userToAdd.CopyProperties(new LoginDtoEntity())
                };

                return response;
            }
        }
        #endregion
        #region Metodos privados
        #endregion
    }
}
