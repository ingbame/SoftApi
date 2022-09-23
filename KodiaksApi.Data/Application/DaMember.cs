using KodiaksApi.Data.Context;
using KodiaksApi.Data.DbModels;
using KodiaksApi.Entity.Application;
using KodiaksApi.Entity.Finance;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Data.Application
{
    public class DaMember
    {
        #region Patron de Diseño
        private static DaMember _instance;
        private static readonly object _instanceLock = new object();
        public static DaMember Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new DaMember();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<MemberSelEntity>> GetMember(long? id = null)
        {
            List<MemberSelEntity> incomesLst = new List<MemberSelEntity>();
            await Task.Run(() =>
            {
                using (var ctx = new DbContextConfig().ExtentionsDbContext())
                {
                    SqlParameter pMovementId = new SqlParameter("@MemberId", SqlDbType.BigInt);
                    pMovementId.Value = !id.HasValue ? DBNull.Value : id;
                    var sqlCmnd = $"EXEC [App].[SPSelMembers] {pMovementId.ParameterName}";

                    incomesLst = ctx.Set<MemberSelEntity>().FromSqlRaw(sqlCmnd, pMovementId).ToList();
                }
            });
            return incomesLst;
        }
    }
}
