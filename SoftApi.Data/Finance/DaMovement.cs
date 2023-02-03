using SoftApi.Data.Context;
using SoftApi.Data.DbModels;
using SoftApi.Entity.Finance;
using SoftApi.Util;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace SoftApi.Data.Finance
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
        public async Task<List<MovementSelEntity>> GetMovement(long? id = null, int? year = null, int? month = null)
        {
            List<MovementSelEntity> incomesLst = new List<MovementSelEntity>();
            await Task.Run(() =>
            {
                using (var ctx = new DbContextConfig().ExtentionsDbContext())
                {
                    SqlParameter pMovementId = new SqlParameter("@MovementId", SqlDbType.BigInt);
                    SqlParameter pYear = new SqlParameter("@Year", SqlDbType.Int);
                    SqlParameter pMonth = new SqlParameter("@Month", SqlDbType.Int);

                    pMovementId.Value = !id.HasValue ? DBNull.Value : id;
                    pYear.Value = !year.HasValue ? DBNull.Value : year;
                    pMonth.Value = !month.HasValue ? DBNull.Value : month;

                    var sqlCmnd = $"EXEC [Fina].[SPSelMovements] {pMovementId.ParameterName}, {pYear.ParameterName}, {pMonth.ParameterName}";

                    incomesLst = ctx.Set<MovementSelEntity>().FromSqlRaw(sqlCmnd, pMovementId, pYear, pMonth).ToList();
                }
            });
            return incomesLst;
        }
        public async Task<List<MovementSelYearMonthEntity>> GetMovementByMonthYear(int? year, int? month)
        {
            List<MovementSelYearMonthEntity> incomesLst = new List<MovementSelYearMonthEntity>();
            await Task.Run(() =>
            {
                using (var ctx = new DbContextConfig().ExtentionsDbContext())
                {
                    SqlParameter pYear = new SqlParameter("@Year", SqlDbType.Int);
                    SqlParameter pMonth = new SqlParameter("@Month", SqlDbType.Int);
                    pYear.Value = !year.HasValue ? DBNull.Value : year;
                    pMonth.Value = !month.HasValue ? DBNull.Value : month;
                    var sqlCmnd = $"EXEC [Fina].[SPSelMovementsByMonthYear] {pYear.ParameterName}, {pMonth.ParameterName}";

                    incomesLst = ctx.Set<MovementSelYearMonthEntity>().FromSqlRaw(sqlCmnd, pYear, pMonth).ToList();
                }
            });
            return incomesLst;
        }

        //[SPSelMovementsByMonthYear]

        public async Task<decimal> GetTotal()
        {
            decimal Total;
            Total = await Task.Run(() =>
            {
                using (var ctx = new DbContextConfig().ExtentionsDbContext())
                {
                    decimal incomeTotal = ctx.Movements.Where(w => w.MovementTypeId == 1).Sum(s => s.Amount);
                    decimal expenseTotal = ctx.Movements.Where(w => w.MovementTypeId == 2).Sum(s => s.Amount);
                    return incomeTotal - expenseTotal;
                }
            });
            return Total;
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
        public async Task<MovementEntity> DeleteMovement(long? MovementId)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var findData = await ctx.Movements.FindAsync(MovementId);
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
