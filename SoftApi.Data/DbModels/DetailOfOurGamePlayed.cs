using System;
using System.Collections.Generic;

namespace SoftApi.Data.DbModels
{
    public partial class DetailOfOurGamePlayed
    {
        public long OurDetailId { get; set; }
        public long MemberId { get; set; }
        public int Inning { get; set; }
        public long DetailId { get; set; }

        public virtual DetailOfGamePlayed Detail { get; set; }
        public virtual Member Member { get; set; }
    }
}
