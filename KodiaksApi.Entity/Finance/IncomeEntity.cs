using KodiaksApi.Entity.Application;
using KodiaksApi.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Entity.Finance
{
    public class IncomeEntity
    {
        public long? IncomeId { get; set; }
        public long? MemberId { get; set; }
        public short? ConceptId { get; set; }
        public short? MethodId { get; set; }
        public DateTime? IncomeDate { get; set; }
        public decimal? Amount { get; set; }
        public string AdditionalComment { get; set; }
        public string EvidenceUrl { get; set; }
        //Parametros de auditoría
        public DateTime? CreatedDate { get; set; }
        public long CreatedBy { get; set; }
    }
}
