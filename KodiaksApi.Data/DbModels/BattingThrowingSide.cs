using System;
using System.Collections.Generic;

namespace SoftApi.Data.DbModels
{
    public partial class BattingThrowingSide
    {
        public BattingThrowingSide()
        {
            Members = new HashSet<Member>();
        }

        public short BtsideId { get; set; }
        public string KeyValue { get; set; }
        public string BtsideDesc { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}
