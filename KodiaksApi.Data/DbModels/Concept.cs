using System;
using System.Collections.Generic;

namespace KodiaksApi.Data.DbModels
{
    public partial class Concept
    {
        public Concept()
        {
            Movements = new HashSet<Movement>();
        }

        public short ConceptId { get; set; }
        public string ConceptKey { get; set; }
        public string ConceptDesc { get; set; }

        public virtual ICollection<Movement> Movements { get; set; }
    }
}
