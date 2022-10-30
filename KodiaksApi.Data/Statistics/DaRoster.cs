using KodiaksApi.Data.Context;
using KodiaksApi.Data.DbModels;
using KodiaksApi.Data.Finance;
using KodiaksApi.Entity.Finance;
using KodiaksApi.Entity.Statistics;
using KodiaksApi.Util;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Data.Statistics
{
    public class DaRoster
    {
        #region Patron de Diseño Sigleton
        private static DaRoster _instance;
        private static readonly object _instanceLock = new object();
        public static DaRoster Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new DaRoster();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<RosterEntity>> GetRoster()
        {
            List<RosterEntity> rosterLst = new List<RosterEntity>();
            await Task.Run(() =>
            {
                using (var ctx = new DbContextConfig().ExtentionsDbContext())
                {

                    var sqlCmnd = $"EXEC [Stats].[SPSelRoster]";

                    rosterLst = ctx.Set<RosterEntity>().FromSqlRaw(sqlCmnd).ToList();
                }
            });
            return rosterLst;
        }
        public async Task<RosterEntity> NewRoster(RosterEntity request)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var roster = request.CopyProperties(new Roster());
                    await ctx.Rosters.AddAsync(roster);
                    await ctx.SaveChangesAsync();
                    await trans.CommitAsync();
                    var response = roster.CopyProperties(new RosterEntity());
                    return response;
                }
            }
        }
        public async Task<RosterEntity> DeleteRoster(long? rosterId)
        {
            using (var ctx = new DbContextConfig().CreateDbContext())
            {
                using (var trans = ctx.Database.BeginTransaction())
                {
                    var findData = await ctx.Rosters.FindAsync(rosterId);
                    if (findData != null)
                    {
                        ctx.Rosters.Remove(findData);
                        await ctx.SaveChangesAsync();
                        await trans.CommitAsync();
                    }
                    else
                        throw new Exception("No se encuentra el valor a eliminar.");
                    var response = findData.CopyProperties(new RosterEntity());
                    return response;
                }
            }
        }
    }
}
