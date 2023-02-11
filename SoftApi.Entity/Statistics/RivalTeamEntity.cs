using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftApi.Entity.Statistics
{
    public class RivalTeamEntity
    {
        public int? RivalTeamId { get; set; }
        public string TeamName { get; set; }
        public bool? IsActive { get; set; }
    }
}
