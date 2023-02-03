using System;
using System.Collections.Generic;

namespace SoftApi.Data.DbModels
{
    public partial class OpposingTeamMember
    {
        public OpposingTeamMember()
        {
            DetailOfTheRivalGamePlayeds = new HashSet<DetailOfTheRivalGamePlayed>();
        }

        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public bool IsPitcher { get; set; }

        public virtual ICollection<DetailOfTheRivalGamePlayed> DetailOfTheRivalGamePlayeds { get; set; }
    }
}
