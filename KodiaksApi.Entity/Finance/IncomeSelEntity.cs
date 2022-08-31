using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Entity.Finance
{
    public class IncomeSelEntity
    {
        public long? IncomeId { get; set; }
        public long? MemberId { get; set; }
		public string FullName { get; set; }
		public short? ConceptId { get; set; }
		public string ConceptKey { get; set; }
		public string ConceptDesc { get; set; }
		public short ConceptTypeId { get; set; }
		public string ConceptTypeKey { get; set; }
		public string ConceptTypeDesc { get; set; }
		public short? MethodId { get; set; }
		public string MethodDesc { get; set; }
		public DateTime? IncomeDate { get; set; }
		public decimal? Amount { get; set; }
		public string AdditionalComment { get; set; }
		public string EvidenceUrl { get; set; }
		public DateTime? CreatedDate { get; set; }
		public long CreatedById { get; set; }
		public string CreatedBy { get; set; }

	}
}
