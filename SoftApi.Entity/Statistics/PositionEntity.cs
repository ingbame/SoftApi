using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftApi.Entity.Statistics
{
    public class PositionEntity
    {
        public short? PositionId { get; set; }
        public string KeyValue { get; set; }
        public string PositionDesc { get; set; }
        public int? Order { get; set; }
    }
}
