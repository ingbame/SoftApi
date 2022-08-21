using System;
using System.Collections.Generic;

namespace KodiaksApi.Data.DbModels
{
    public partial class Concept
    {
        public Concept()
        {
            Bills = new HashSet<Bill>();
            Incomes = new HashSet<Income>();
        }

        public short ConceptId { get; set; }
        public short ConceptTypeId { get; set; }
        public string ConceptKey { get; set; }
        public string ConceptDesc { get; set; }

        public virtual ConceptType ConceptType { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Income> Incomes { get; set; }
    }
}
