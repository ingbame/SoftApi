using KodiaksApi.Data.Context;
using KodiaksApi.Data.DbModels;
using KodiaksApi.Entity.Finance;
using KodiaksApi.Entity.Security;
using KodiaksApi.Util;
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
        public List<IncomeSelEntity> GetIncomes()
        {
            using (var ctx = new DbContextConfig().ExtentionsDbContext())
            {
                var result = ctx.Set<IncomeSelEntity>().ToList();
                return result;
            }
        }
        public IncomeEntity NewIncome(IncomeEntity request)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var income = request.CopyProperties(new Income());
                    var addIncome = ctx.Incomes.Add(income);
                    if (addIncome.State != EntityState.Added)
                        throw new Exception("No se pudo agregar correctamente el ingreso.");
                    ctx.SaveChanges();
                    trans.Commit();
                    var response = income.CopyProperties(new IncomeEntity());
                    return response;
                }
            }
        }
        #endregion
        #region Metodos privados
        #endregion
    }
}
