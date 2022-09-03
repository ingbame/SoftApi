using KodiaksApi.Data.Context;
using KodiaksApi.Data.DbModels;
using KodiaksApi.Entity.Finance;
using KodiaksApi.Util;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
                    var sqlCmnd = "EXEC [Fina].[SPSelMovements]";
                    SqlParameter param = null;
                    if (id.HasValue)
                    {
                        param = new SqlParameter("@MovementId", id.Value);
                        sqlCmnd += param.ParameterName;
                    }

                    incomesLst = ctx.Set<MovementSelEntity>().FromSqlRaw(sqlCmnd, param).ToList();
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
                    var income = request.CopyProperties(new Movement());
                    var addIncome = await ctx.Movements.AddAsync(income);
                    if (addIncome.State != EntityState.Added)
                        throw new Exception("No se pudo agregar correctamente el ingreso.");
                    await ctx.SaveChangesAsync();
                    await trans.CommitAsync();
                    var response = income.CopyProperties(new MovementEntity());
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
                    var income = request.CopyProperties(new Movement());

                    ctx.Movements.Update(income);
                    await ctx.SaveChangesAsync();
                    await trans.CommitAsync();

                    var response = income.CopyProperties(new MovementEntity());
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
                        ctx.Remove(findData);
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
