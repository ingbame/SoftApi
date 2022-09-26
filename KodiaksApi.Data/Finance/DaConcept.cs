using KodiaksApi.Data.Context;
using KodiaksApi.Entity.Finance;
using KodiaksApi.Util;

namespace KodiaksApi.Data.Finance
{
    public class DaConcept
    {
        #region Patron de Diseño Sigleton
        private static DaConcept _instance;
        private static readonly object _instanceLock = new object();
        public static DaConcept Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new DaConcept();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<ConceptEntity>> GetConcept(short? id = null)
        {
            List<ConceptEntity> types = new List<ConceptEntity>();

            using (var ctx = new DbContextConfig().ExtentionsDbContext())
            {
                if (id.HasValue)
                {
                    var search = await ctx.Concepts.FindAsync(id);
                    types.Add(search.CopyProperties(new ConceptEntity()));
                }
                else
                    types = ctx.Concepts.Select(s => s.CopyProperties(new ConceptEntity())).ToList();
            }
            return types;
        }
    }
}
