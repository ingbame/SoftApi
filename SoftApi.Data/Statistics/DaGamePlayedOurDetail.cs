using Microsoft.EntityFrameworkCore;
using SoftApi.Data.Context;
using SoftApi.Data.DbModels;
using SoftApi.Entity.Statistics;
using SoftApi.Util;

namespace SoftApi.Data.Statistics
{
    public class DaGamePlayedOurDetail
    {
        #region Patron de Diseño Sigleton
        private static DaGamePlayedOurDetail _instance;
        private static readonly object _instanceLock = new object();
        public static DaGamePlayedOurDetail Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new DaGamePlayedOurDetail();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<GamePlayedOurDetailEntity>> GetGamePlayedOurDetail(long? id = null)
        {
            List<GamePlayedOurDetailEntity> incomesLst = new List<GamePlayedOurDetailEntity>();

            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                if (id.HasValue)
                {
                    var search = await ctx.DetailOfOurGamePlayeds.Join(
                            ctx.DetailOfGamePlayeds,
                            dougp => dougp.DetailId, dogp => dogp.DetailId,
                            (dougp, dogp) => new { Dougp = dougp, Dogp = dogp }
                        )
                        .Where(w => w.Dougp.OurDetailId == id.Value).FirstOrDefaultAsync();
                    var convert = search.CopyProperties(new GamePlayedOurDetailEntity());
                    incomesLst.Add(convert);
                }
                else
                    incomesLst = ctx.DetailOfOurGamePlayeds.Select(s => s.CopyProperties(new GamePlayedOurDetailEntity())).ToList();
            }
            return incomesLst;
        }
        public async Task<List<GamePlayedOurDetailEntity>> GetGamePlayedOurDetailByGame(int? id = null)
        {
            List<GamePlayedOurDetailEntity> incomesLst = new List<GamePlayedOurDetailEntity>();

            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                if (id.HasValue)
                {
                    var search = await ctx.DetailOfOurGamePlayeds.Join(
                            ctx.DetailOfGamePlayeds,
                            dougp => dougp.DetailId, dogp => dogp.DetailId,
                            (dougp, dogp) => new { Dougp = dougp, Dogp = dogp }
                        )
                        .Where(w => w.Dogp.GameId == id.Value).FirstOrDefaultAsync();
                    var convert = search.CopyProperties(new GamePlayedOurDetailEntity());
                    incomesLst.Add(convert);
                }
                else
                    incomesLst = ctx.DetailOfOurGamePlayeds.Select(s => s.CopyProperties(new GamePlayedOurDetailEntity())).ToList();
            }
            return incomesLst;
        }
        public async Task<GamePlayedOurDetailEntity> NewGamePlayedOurDetail(GamePlayedOurDetailEntity request)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var dataToAdd = request.CopyProperties(new DetailOfOurGamePlayed());
                    await ctx.DetailOfOurGamePlayeds.AddAsync(dataToAdd);
                    await ctx.SaveChangesAsync();
                    await trans.CommitAsync();
                    var response = dataToAdd.CopyProperties(new GamePlayedOurDetailEntity());
                    return response;
                }
            }
        }
        public async Task<GamePlayedOurDetailEntity> EditGamePlayedOurDetail(GamePlayedOurDetailEntity request)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var dataToUpdate = request.CopyProperties(new DetailOfOurGamePlayed());
                    ctx.Entry(dataToUpdate).State = EntityState.Modified;
                    ctx.Entry(dataToUpdate).Property(x => x.OurDetailId).IsModified = false;
                    try
                    {
                        await ctx.SaveChangesAsync();
                        await trans.CommitAsync();
                    }
                    catch (DbUpdateConcurrencyException) //Error al hacer el update en caso de que se elimine por otro usuario en el proceso
                    {
                        if (!await ctx.DetailOfOurGamePlayeds.AnyAsync(e => e.OurDetailId == request.OurDetailId))
                            throw new Exception("No se pudo actualizar, el registro ya no existe.");
                        else
                            throw new Exception("No se pudo actualizar, revise la información.");
                    }
                    var response = dataToUpdate.CopyProperties(new GamePlayedOurDetailEntity());
                    return response;
                }
            }
        }
        public async Task<GamePlayedOurDetailEntity> DeleteGamePlayedOurDetail(long? id)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var findData = await ctx.DetailOfOurGamePlayeds.FindAsync(id);
                    if (findData != null)
                    {
                        ctx.DetailOfOurGamePlayeds.Remove(findData);
                        await ctx.SaveChangesAsync();
                        await trans.CommitAsync();
                    }
                    else
                        throw new Exception("No se encuentra el valor a eliminar.");
                    var response = findData.CopyProperties(new GamePlayedOurDetailEntity());
                    return response;
                }
            }
        }
    }
}
