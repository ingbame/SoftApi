﻿using Microsoft.EntityFrameworkCore;
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
    public class DaRivalTeam
    {
        #region Patron de Diseño Sigleton
        private static DaRivalTeam _instance;
        private static readonly object _instanceLock = new object();
        public static DaRivalTeam Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new DaRivalTeam();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<RivalTeamEntity>> GetRivalTeam(int? id = null)
        {
            List<RivalTeamEntity> incomesLst = new List<RivalTeamEntity>();

            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                if (id.HasValue)
                {
                    var search = await ctx.RivalTeams.FindAsync(id);
                    incomesLst.Add(search.CopyProperties(new RivalTeamEntity()));
                }
                else
                    incomesLst = ctx.RivalTeams.Select(s => s.CopyProperties(new RivalTeamEntity())).ToList();
            }
            return incomesLst;
        }
        public async Task<RivalTeamEntity> NewRivalTeam(RivalTeamEntity request)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var dataToAdd = request.CopyProperties(new RivalTeam());
                    await ctx.RivalTeams.AddAsync(dataToAdd);
                    await ctx.SaveChangesAsync();
                    await trans.CommitAsync();
                    var response = dataToAdd.CopyProperties(new RivalTeamEntity());
                    return response;
                }
            }
        }
        public async Task<RivalTeamEntity> EditRivalTeam(RivalTeamEntity request)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var dataToUpdate = request.CopyProperties(new RivalTeam());
                    ctx.Entry(dataToUpdate).State = EntityState.Modified;
                    ctx.Entry(dataToUpdate).Property(x => x.RivalTeamId).IsModified = false;
                    try
                    {
                        await ctx.SaveChangesAsync();
                        await trans.CommitAsync();
                    }
                    catch (DbUpdateConcurrencyException) //Error al hacer el update en caso de que se elimine por otro usuario en el proceso
                    {
                        if (!await ctx.RivalTeams.AnyAsync(e => e.RivalTeamId == request.RivalTeamId))
                            throw new Exception("No se pudo actualizar, el registro ya no existe.");
                        else
                            throw new Exception("No se pudo actualizar, revise la información.");
                    }
                    var response = dataToUpdate.CopyProperties(new RivalTeamEntity());
                    return response;
                }
            }
        }
        public async Task<RivalTeamEntity> DeleteRivalTeam(int? RivalTeamId)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var findData = await ctx.RivalTeams.FindAsync(RivalTeamId);
                    if (findData != null)
                    {
                        ctx.RivalTeams.Remove(findData);
                        await ctx.SaveChangesAsync();
                        await trans.CommitAsync();
                    }
                    else
                        throw new Exception("No se encuentra el valor a eliminar.");
                    var response = findData.CopyProperties(new RivalTeamEntity());
                    return response;
                }
            }
        }
    }
}
