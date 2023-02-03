using System;
using System.Collections.Generic;

namespace SoftApi.Data.DbModels
{
    public partial class Movement
    {
        public long MovementId { get; set; }
        public long MemberId { get; set; }
        public short MovementTypeId { get; set; }
        public short ConceptId { get; set; }
        public short MethodId { get; set; }
        public DateTime MovementDate { get; set; }
        public decimal Amount { get; set; }
        public string AdditionalComment { get; set; }
        public string EvidenceUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }

        public virtual Concept Concept { get; set; }
        public virtual User CreatedByNavigation { get; set; }
        public virtual Member Member { get; set; }
        public virtual PaymentMethod Method { get; set; }
        public virtual MovementType MovementType { get; set; }
    }
}
