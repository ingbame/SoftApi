using KodiaksApi.Data.Context;
using KodiaksApi.Data.DbModels;
using KodiaksApi.Entity.Finance;
using KodiaksApi.Util;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace KodiaksApi.Data.Finance
{
    public class DaMovement
    {
        #region Patron de Diseño Sigleton
        private static DaMovement _instance;
        private static readonly object _instanceLock = new object();
        public static DaMovement Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new DaMovement();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<MovementSelEntity>> GetMovement(long? id = null)
        {
            List<MovementSelEntity> incomesLst = new List<MovementSelEntity>();
            await Task.Run(() =>
            {
                using (var ctx = new DbContextConfig().ExtentionsDbContext())
                {
                    SqlParameter pMovementId = new SqlParameter("@MovementId", SqlDbType.BigInt);
                    pMovementId.Value = !id.HasValue ? DBNull.Value : id;
                    var sqlCmnd = $"EXEC [Fina].[SPSelMovements] {pMovementId.ParameterName}";

                    incomesLst = ctx.Set<MovementSelEntity>().FromSqlRaw(sqlCmnd, pMovementId).ToList();
                }
            });
            return incomesLst;
        }
        public async Task<MovementEntity> NewMovement(MovementEntity request)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var movement = request.CopyProperties(new Movement());
                    await ctx.Movements.AddAsync(movement);
                    await ctx.SaveChangesAsync();
                    await trans.CommitAsync();
                    var response = movement.CopyProperties(new MovementEntity());
                    return response;
                }
            }
        }
        public async Task<MovementEntity> EditMovement(MovementEntity request)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var movement = request.CopyProperties(new Movement());
                    ctx.Entry(movement).State = EntityState.Modified;
                    ctx.Entry(movement).Property(x => x.CreatedBy).IsModified = false;
                    ctx.Entry(movement).Property(x => x.CreatedDate).IsModified = false;
                    try
                    {
                        await ctx.SaveChangesAsync();
                        await trans.CommitAsync();
                    }
                    catch (DbUpdateConcurrencyException) //Error al hacer el update en caso de que se elimine por otro usuario en el proceso
                    {
                        if (!await ctx.Movements.AnyAsync(e => e.MovementId == request.MovementId))
                            throw new Exception("No se pudo actualizar, el registro ya no existe.");
                        else
                            throw new Exception("No se pudo actualizar, revise la información.");
                    }
                    var response = movement.CopyProperties(new MovementEntity());
                    return response;
                }
            }
        }
        public async Task<MovementEntity> DeleteMovement(long? incomeId)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var findData = await ctx.Movements.FindAsync(incomeId.Value);
                    if (findData != null)
                    {
                        ctx.Movements.Remove(findData);
                        await ctx.SaveChangesAsync();
                        await trans.CommitAsync();
                    }
                    else
                        throw new Exception("No se encuentra el valor a eliminar.");
                    var response = findData.CopyProperties(new MovementEntity());
                    return response;
                }
            }
        }

    }
}
