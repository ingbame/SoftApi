using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Entity.Finance
{
    public class BillEntity
    {
        public long? BillId { get; set; }
        public long? MemberId { get; set; }
        public short? ConceptId { get; set; }
        public short? MethodId { get; set; }
        public DateTime? IncomeDate { get; set; }
        public decimal? Amount { get; set; }
        public string AdditionalComment { get; set; }
        public string EvidenceUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
    }
}
