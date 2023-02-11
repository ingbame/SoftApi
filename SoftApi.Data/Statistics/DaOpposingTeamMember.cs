using Microsoft.EntityFrameworkCore;
using SoftApi.Data.Context;
using SoftApi.Data.DbModels;
using SoftApi.Entity.Finance;
using SoftApi.Entity.Statistics;
using SoftApi.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftApi.Data.Statistics
{
    public class DaOpposingTeamMember
    {
        #region Patron de Diseño Sigleton
        private static DaOpposingTeamMember _instance;
        private static readonly object _instanceLock = new object();
        public static DaOpposingTeamMember Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new DaOpposingTeamMember();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<OpposingTeamMemberEntity>> GetTeamMember(int? id = null)
        {
            List<OpposingTeamMemberEntity> incomesLst = new List<OpposingTeamMemberEntity>();

            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                if (id.HasValue)
                {
                    var search = await ctx.OpposingTeamMembers.FindAsync(id);
                    incomesLst.Add(search.CopyProperties(new OpposingTeamMemberEntity()));
                }
                else
                    incomesLst = ctx.OpposingTeamMembers.Select(s => s.CopyProperties(new OpposingTeamMemberEntity())).ToList();
            }
            return incomesLst;
        }
        public async Task<OpposingTeamMemberEntity> NewTeamMember(OpposingTeamMemberEntity request)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var dataToAdd = request.CopyProperties(new OpposingTeamMember());
                    await ctx.OpposingTeamMembers.AddAsync(dataToAdd);
                    await ctx.SaveChangesAsync();
                    await trans.CommitAsync();
                    var response = dataToAdd.CopyProperties(new OpposingTeamMemberEntity());
                    return response;
                }
            }
        }
        public async Task<OpposingTeamMemberEntity> EditTeamMember(OpposingTeamMemberEntity request)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var dataToUpdate = request.CopyProperties(new OpposingTeamMember());
                    ctx.Entry(dataToUpdate).State = EntityState.Modified;
                    ctx.Entry(dataToUpdate).Property(x => x.MemberId).IsModified = false;
                    try
                    {
                        await ctx.SaveChangesAsync();
                        await trans.CommitAsync();
                    }
                    catch (DbUpdateConcurrencyException) //Error al hacer el update en caso de que se elimine por otro usuario en el proceso
                    {
                        if (!await ctx.OpposingTeamMembers.AnyAsync(e => e.MemberId == request.MemberId))
                            throw new Exception("No se pudo actualizar, el registro ya no existe.");
                        else
                            throw new Exception("No se pudo actualizar, revise la información.");
                    }
                    var response = dataToUpdate.CopyProperties(new OpposingTeamMemberEntity());
                    return response;
                }
            }
        }
        public async Task<OpposingTeamMemberEntity> DeleteTeamMember(int? MemberId)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var findData = await ctx.OpposingTeamMembers.FindAsync(MemberId);
                    if (findData != null)
                    {
                        ctx.OpposingTeamMembers.Remove(findData);
                        await ctx.SaveChangesAsync();
                        await trans.CommitAsync();
                    }
                    else
                        throw new Exception("No se encuentra el valor a eliminar.");
                    var response = findData.CopyProperties(new OpposingTeamMemberEntity());
                    return response;
                }
            }
        }
    }
}
