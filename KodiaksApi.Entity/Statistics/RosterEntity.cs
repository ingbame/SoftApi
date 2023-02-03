using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftApi.Entity.Statistics
{
    public class RosterEntity
    {
        public long? RosterId { get; set; }
        public long? MemberId { get; set; }
        public string FullName { get; set; }
        public int? ShirtNumber { get; set; }
        public short? BTSideId { get; set; }
        public string BTKeyValue { get; set; }
        public string BTSideDesc { get; set; }
        public DateTime? Birthday { get; set; }
        public short? PositionId { get; set; }
        public string PKeyValue { get; set; }
        public string PositionDesc { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ByUser { get; set; }
        public long? CreatedBy { get; set; }
    }
}
