using KodiaksApi.Data.Finance;
using KodiaksApi.Entity.Finance;

namespace KodiaksApi.Core.Finance
{
    public class BoConcept
    {
        #region Patron de Diseño
        private static BoConcept _instance;
        private static readonly object _instanceLock = new object();
        public static BoConcept Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new BoConcept();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public async Task<List<ConceptEntity>> GetConcept(short? id = null)
        {
            var response = await DaConcept.Instance.GetConcept(id);
            return response;
        }
    }
}
