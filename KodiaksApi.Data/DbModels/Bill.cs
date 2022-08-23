using System;
using System.Collections.Generic;

namespace KodiaksApi.Data.DbModels
{
    public partial class Bill
    {
        public long BillId { get; set; }
        public long MemberId { get; set; }
        public short ConceptId { get; set; }
        public short MethodId { get; set; }
        public DateTime IncomeDate { get; set; }
        public decimal Amount { get; set; }
        public string AdditionalComment { get; set; }
        public string EvidenceUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }

        public virtual Concept Concept { get; set; }
        public virtual User CreatedByNavigation { get; set; }
        public virtual Member Member { get; set; }
        public virtual PaymentMethod Method { get; set; }
    }
}
