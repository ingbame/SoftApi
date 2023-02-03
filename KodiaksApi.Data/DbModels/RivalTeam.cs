using System;
using System.Collections.Generic;

namespace SoftApi.Data.DbModels
{
    public partial class RivalTeam
    {
        public RivalTeam()
        {
            GamesPlayeds = new HashSet<GamesPlayed>();
        }

        public int RivalTeamId { get; set; }
        public string TeamName { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<GamesPlayed> GamesPlayeds { get; set; }
    }
}
