using System;
using System.Collections.Generic;

namespace SoftApi.Data.DbModels
{
    public partial class DetailOfTheRivalGamePlayed
    {
        public long RivalDetailId { get; set; }
        public int MemberId { get; set; }
        public int Inning { get; set; }
        public long DetailId { get; set; }

        public virtual DetailOfGamePlayed Detail { get; set; }
        public virtual OpposingTeamMember Member { get; set; }
    }
}
