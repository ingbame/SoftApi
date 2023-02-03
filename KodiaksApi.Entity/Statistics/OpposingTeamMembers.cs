using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftApi.Entity.Statistics
{
    public class OpposingTeamMembers
    {
        public int? MemberId { get; set; }
        public string MemberName { get; set; }
        public bool? IsPitcher { get; set; }
    }
}
