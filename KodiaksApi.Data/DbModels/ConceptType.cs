using System;
using System.Collections.Generic;

namespace KodiaksApi.Data.DbModels
{
    public partial class ConceptType
    {
        public ConceptType()
        {
            Concepts = new HashSet<Concept>();
        }

        public short ConceptTypeId { get; set; }
        public string ConceptTypeKey { get; set; }
        public string ConceptTypeDesc { get; set; }

        public virtual ICollection<Concept> Concepts { get; set; }
    }
}
