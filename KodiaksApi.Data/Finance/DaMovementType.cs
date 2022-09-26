using KodiaksApi.Data.Context;
using KodiaksApi.Entity.Finance;
using KodiaksApi.Entity.Statistics;
using KodiaksApi.Util;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Data.Finance
{
    public class DaMovementType
    {
        #region Patron de Diseño Sigleton
        private static DaMovementType _instance;
        private static readonly object _instanceLock = new object();
        public static DaMovementType Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new DaMovementType();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<MovementTypeEntity>> GetMovementType(short? id = null)
        {
            List<MovementTypeEntity> types = new List<MovementTypeEntity>();

            using (var ctx = new DbContextConfig().ExtentionsDbContext())
            {
                if (id.HasValue) {
                    var search = await ctx.MovementTypes.FindAsync(id);
                    types.Add(search.CopyProperties(new MovementTypeEntity()));
                }
                else
                    types = ctx.MovementTypes.Select(s => s.CopyProperties(new MovementTypeEntity())).ToList();
            }
            return types;
        }
    }
}
