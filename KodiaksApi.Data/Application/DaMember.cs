using KodiaksApi.Data.Context;
using KodiaksApi.Data.DbModels;
using KodiaksApi.Entity.Application;
using KodiaksApi.Entity.Finance;
using KodiaksApi.Entity.Security;
using KodiaksApi.Util;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Data.Application
{
    public class DaMember
    {
        #region Patron de Diseño
        private static DaMember _instance;
        private static readonly object _instanceLock = new object();
        public static DaMember Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new DaMember();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<MemberSelEntity>> GetMember(long? id = null)
        {
            List<MemberSelEntity> incomesLst = new List<MemberSelEntity>();
            await Task.Run(() =>
            {
                using (var ctx = new DbContextConfig().ExtentionsDbContext())
                {
                    SqlParameter pMovementId = new SqlParameter("@MemberId", SqlDbType.BigInt);
                    pMovementId.Value = !id.HasValue ? DBNull.Value : id;
                    var sqlCmnd = $"EXEC [App].[SPSelMembers] {pMovementId.ParameterName}";

                    incomesLst = ctx.Set<MemberSelEntity>().FromSqlRaw(sqlCmnd, pMovementId).ToList();
                }
            });
            return incomesLst;
        }
        public async Task<CredentialsDtoEntity> CreateMember(CredentialsEntity newUser, string salt, string password)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                var personaToAdd = newUser.Member.CopyProperties(new Member());
                var getDefaultRole = ctx.Roles.Where(w => w.RoleDescription.Equals("User")).FirstOrDefault();
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
                var addUser = await ctx.Users.AddAsync(userToAdd);
                if (addUser.State != EntityState.Added)
                    throw new Exception("No se agregó el usuario correctamente");
                ctx.SaveChanges();

                //Validar que la persona exista, pendiente
                personaToAdd.UserId = userToAdd.UserId;
                var addPersona = await ctx.Members.AddAsync(personaToAdd);
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
        public async Task<CredentialsDtoEntity> EditMember(CredentialsEntity request)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var member = request.Member.CopyProperties(new Member());
                    var user = request.User.CopyProperties(new User());

                    ctx.Entry(member).State = EntityState.Modified;
                    ctx.Entry(member).Property(x => x.UserId).IsModified = false;

                    ctx.Entry(user).State = EntityState.Modified;
                    ctx.Entry(user).Property(x => x.Password).IsModified = false;
                    ctx.Entry(user).Property(x => x.PasswordSalt).IsModified = false;
                    ctx.Entry(user).Property(x => x.SavePasswords).IsModified = false;
                    ctx.Entry(user).Property(x => x.CreatedDate).IsModified = false;

                    try
                    {
                        await ctx.SaveChangesAsync();
                        await trans.CommitAsync();
                    }
                    catch (DbUpdateConcurrencyException) //Error al hacer el update en caso de que se elimine por otro usuario en el proceso
                    {
                        if (!await ctx.Members.AnyAsync(e => e.MemberId == request.Member.MemberId))
                            throw new Exception("No se pudo actualizar, el registro ya no existe.");
                        else if (!await ctx.Users.AnyAsync(e => e.UserId == request.User.UserId))
                            throw new Exception("No se pudo actualizar, el registro ya no existe.");
                        else
                            throw new Exception("No se pudo actualizar, revise la información.");
                    }
                    var response = new CredentialsDtoEntity();
                    response.Member = member.CopyProperties(new MemberEntity());
                    response.User = user.CopyProperties(new LoginDtoEntity());

                    return response;
                }
            }
        }
    }
}
