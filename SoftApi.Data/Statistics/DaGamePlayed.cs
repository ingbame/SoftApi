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
    public class DaGamePlayed
    {
        #region Patron de Diseño Sigleton
        private static DaGamePlayed _instance;
        private static readonly object _instanceLock = new object();
        public static DaGamePlayed Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new DaGamePlayed();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<GamePlayedEntity>> GetGamePlayed(int? id = null)
        {
            List<GamePlayedEntity> incomesLst = new List<GamePlayedEntity>();

            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                if (id.HasValue)
                {
                    var search = await ctx.GamesPlayeds.FindAsync(id);
                    incomesLst.Add(search.CopyProperties(new GamePlayedEntity()));
                }
                else
                    incomesLst = ctx.GamesPlayeds.Select(s => s.CopyProperties(new GamePlayedEntity())).ToList();
            }
            return incomesLst;
        }
        public async Task<GamePlayedEntity> NewGamePlayed(GamePlayedEntity request)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var dataToAdd = request.CopyProperties(new GamesPlayed());
                    await ctx.GamesPlayeds.AddAsync(dataToAdd);
                    await ctx.SaveChangesAsync();
                    await trans.CommitAsync();
                    var response = dataToAdd.CopyProperties(new GamePlayedEntity());
                    return response;
                }
            }
        }
        public async Task<GamePlayedEntity> EditGamePlayed(GamePlayedEntity request)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var dataToUpdate = request.CopyProperties(new GamesPlayed());
                    ctx.Entry(dataToUpdate).State = EntityState.Modified;
                    ctx.Entry(dataToUpdate).Property(x => x.GameId).IsModified = false;
                    try
                    {
                        await ctx.SaveChangesAsync();
                        await trans.CommitAsync();
                    }
                    catch (DbUpdateConcurrencyException) //Error al hacer el update en caso de que se elimine por otro usuario en el proceso
                    {
                        if (!await ctx.GamesPlayeds.AnyAsync(e => e.GameId == request.GameId))
                            throw new Exception("No se pudo actualizar, el registro ya no existe.");
                        else
                            throw new Exception("No se pudo actualizar, revise la información.");
                    }
                    var response = dataToUpdate.CopyProperties(new GamePlayedEntity());
                    return response;
                }
            }
        }
        public async Task<GamePlayedEntity> DeleteGamePlayed(int? GameId)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var findData = await ctx.GamesPlayeds.FindAsync(GameId);
                    if (findData != null)
                    {
                        ctx.GamesPlayeds.Remove(findData);
                        await ctx.SaveChangesAsync();
                        await trans.CommitAsync();
                    }
                    else
                        throw new Exception("No se encuentra el valor a eliminar.");
                    var response = findData.CopyProperties(new GamePlayedEntity());
                    return response;
                }
            }
        }
    }
}
