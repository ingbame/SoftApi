using KodiaksApi.Data.Context;
using KodiaksApi.Data.DbModels;
using KodiaksApi.Entity.Finance;
using KodiaksApi.Entity.Security;
using KodiaksApi.Util;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Data
{
    public class DaFinance
    {
        #region Patron de Diseño Sigleton
        private static DaFinance _instance;
        private static readonly object _instanceLock = new object();
        public static DaFinance Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new DaFinance();
                    }
                }
                return _instance;
            }
        }
        #endregion
        #region Metodos publicos  
        public async Task<List<IncomeSelEntity>> GetIncomes(int? id = null)
        {
            List<IncomeSelEntity> incomesLst = new List<IncomeSelEntity>();
            await Task.Run(() =>
            {
                using (var ctx = new DbContextConfig().ExtentionsDbContext())
                {
                    var sqlCmnd = "EXEC [Fina].[SPSelIncomes]";
                    SqlParameter param = null;
                    if (id.HasValue)
                    {
                        param = new SqlParameter("@IncomeId", id.Value);
                        sqlCmnd += param.ParameterName;
                    }

                    incomesLst = ctx.Set<IncomeSelEntity>().FromSqlRaw(sqlCmnd, param).ToList();
                }
            });
            return incomesLst;
        }
        public async Task<IncomeEntity> NewIncome(IncomeEntity request)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var income = request.CopyProperties(new Income());
                    var addIncome = await ctx.Incomes.AddAsync(income);
                    if (addIncome.State != EntityState.Added)
                        throw new Exception("No se pudo agregar correctamente el ingreso.");
                    await ctx.SaveChangesAsync();
                    await trans.CommitAsync();
                    var response = income.CopyProperties(new IncomeEntity());
                    return response;
                }
            }
        }
        public async Task<IncomeEntity> EditIncome(IncomeEntity request)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var income = request.CopyProperties(new Income());
                    //var findData = await ctx.Incomes.FindAsync(request.IncomeId);
                    //if (findData != null)
                    //{
                    //    findData.MemberId = request.MemberId.Value;
                    //    findData.ConceptId = request.ConceptId.Value;
                    //    findData.MethodId = request.MethodId.Value;
                    //    findData.IncomeDate = request.IncomeDate.Value;
                    //    findData.Amount = request.Amount.Value;
                    //    findData.AdditionalComment = request.AdditionalComment;
                    //    findData.EvidenceUrl = request.EvidenceUrl;
                    //    await ctx.SaveChangesAsync();
                    //    await trans.CommitAsync();
                    //}

                    ctx.Incomes.Update(income);
                    await ctx.SaveChangesAsync();
                    await trans.CommitAsync();

                    var response = income.CopyProperties(new IncomeEntity());
                    return response;
                }
            }
        }
        public async Task<IncomeEntity> DeleteIncome(long? incomeId)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var findData = await ctx.Incomes.FindAsync(incomeId.Value);
                    if (findData != null)
                    {
                        ctx.Remove(findData);
                        await ctx.SaveChangesAsync();
                        await trans.CommitAsync();
                    }
                    else
                        throw new Exception("No se encuentra el valor a eliminar.");

                    var response = findData.CopyProperties(new IncomeEntity());
                    return response;
                }
            }
        }
        #endregion
        #region Metodos privados
        #endregion
    }
}
