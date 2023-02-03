using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftApi.Entity.Finance
{
    public class MovementSelEntity
    {
        public long? MovementId { get; set; }
        public long? MemberId { get; set; }
		public string FullName { get; set; }
		public short? ConceptId { get; set; }
		public string ConceptKey { get; set; }
		public string ConceptDesc { get; set; }
		public short MovementTypeId { get; set; }
		public string MovementTypeKey { get; set; }
		public string MovementTypeDesc { get; set; }
		public short? MethodId { get; set; }
		public string MethodDesc { get; set; }
		public DateTime? MovementDate { get; set; }
		public decimal? Amount { get; set; }
		public string AdditionalComment { get; set; }
		public string EvidenceUrl { get; set; }
		public DateTime? CreatedDate { get; set; }
		public long CreatedById { get; set; }
		public string CreatedBy { get; set; }

	}
}
