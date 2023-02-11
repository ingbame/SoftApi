using Microsoft.EntityFrameworkCore;
using SoftApi.Data.Context;
using SoftApi.Data.DbModels;
using SoftApi.Entity.Statistics;
using SoftApi.Util;

namespace SoftApi.Data.Statistics
{
    public class DaGamePlayedRivalDetail
    {
        #region Patron de Diseño Sigleton
        private static DaGamePlayedRivalDetail _instance;
        private static readonly object _instanceLock = new object();
        public static DaGamePlayedRivalDetail Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new DaGamePlayedRivalDetail();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<GamePlayedRivalDetailEntity>> GetGamePlayedRivalDetail(long? id = null)
        {
            List<GamePlayedRivalDetailEntity> incomesLst = new List<GamePlayedRivalDetailEntity>();

            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                if (id.HasValue)
                {
                    var search = await ctx.DetailOfTheRivalGamePlayeds.Join(
                            ctx.DetailOfGamePlayeds,
                            dougp => dougp.DetailId, dogp => dogp.DetailId,
                            (dougp, dogp) => new { Dougp = dougp, Dogp = dogp }
                        )
                        .Where(w => w.Dougp.RivalDetailId == id.Value).FirstOrDefaultAsync();
                    var convert = search.CopyProperties(new GamePlayedRivalDetailEntity());
                    incomesLst.Add(convert);
                }
                else
                    incomesLst = ctx.DetailOfTheRivalGamePlayeds.Select(s => s.CopyProperties(new GamePlayedRivalDetailEntity())).ToList();
            }
            return incomesLst;
        }
        public async Task<List<GamePlayedRivalDetailEntity>> GetGamePlayedRivalDetailByGame(int? id = null)
        {
            List<GamePlayedRivalDetailEntity> incomesLst = new List<GamePlayedRivalDetailEntity>();

            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                if (id.HasValue)
                {
                    var search = await ctx.DetailOfTheRivalGamePlayeds.Join(
                            ctx.DetailOfGamePlayeds,
                            dougp => dougp.DetailId, dogp => dogp.DetailId,
                            (dougp, dogp) => new { Dougp = dougp, Dogp = dogp }
                        )
                        .Where(w => w.Dogp.GameId == id.Value).FirstOrDefaultAsync();
                    var convert = search.CopyProperties(new GamePlayedRivalDetailEntity());
                    incomesLst.Add(convert);
                }
                else
                    incomesLst = ctx.DetailOfTheRivalGamePlayeds.Select(s => s.CopyProperties(new GamePlayedRivalDetailEntity())).ToList();
            }
            return incomesLst;
        }
        public async Task<GamePlayedRivalDetailEntity> NewGamePlayedRivalDetail(GamePlayedRivalDetailEntity request)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var dataToAdd = request.CopyProperties(new DetailOfTheRivalGamePlayed());
                    await ctx.DetailOfTheRivalGamePlayeds.AddAsync(dataToAdd);
                    await ctx.SaveChangesAsync();
                    await trans.CommitAsync();
                    var response = dataToAdd.CopyProperties(new GamePlayedRivalDetailEntity());
                    return response;
                }
            }
        }
        public async Task<GamePlayedRivalDetailEntity> EditGamePlayedRivalDetail(GamePlayedRivalDetailEntity request)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var dataToUpdate = request.CopyProperties(new DetailOfTheRivalGamePlayed());
                    ctx.Entry(dataToUpdate).State = EntityState.Modified;
                    ctx.Entry(dataToUpdate).Property(x => x.RivalDetailId).IsModified = false;
                    try
                    {
                        await ctx.SaveChangesAsync();
                        await trans.CommitAsync();
                    }
                    catch (DbUpdateConcurrencyException) //Error al hacer el update en caso de que se elimine por otro usuario en el proceso
                    {
                        if (!await ctx.DetailOfTheRivalGamePlayeds.AnyAsync(e => e.RivalDetailId == request.RivalDetailId))
                            throw new Exception("No se pudo actualizar, el registro ya no existe.");
                        else
                            throw new Exception("No se pudo actualizar, revise la información.");
                    }
                    var response = dataToUpdate.CopyProperties(new GamePlayedRivalDetailEntity());
                    return response;
                }
            }
        }
        public async Task<GamePlayedRivalDetailEntity> DeleteGamePlayedRivalDetail(long? id)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var findData = await ctx.DetailOfTheRivalGamePlayeds.FindAsync(id);
                    if (findData != null)
                    {
                        ctx.DetailOfTheRivalGamePlayeds.Remove(findData);
                        await ctx.SaveChangesAsync();
                        await trans.CommitAsync();
                    }
                    else
                        throw new Exception("No se encuentra el valor a eliminar.");
                    var response = findData.CopyProperties(new GamePlayedRivalDetailEntity());
                    return response;
                }
            }
        }
    }
}
