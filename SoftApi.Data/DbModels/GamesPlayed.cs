using System;
using System.Collections.Generic;

namespace SoftApi.Data.DbModels
{
    public partial class GamesPlayed
    {
        public GamesPlayed()
        {
            DetailOfGamePlayeds = new HashSet<DetailOfGamePlayed>();
        }

        public int GameId { get; set; }
        public int RivalTeamId { get; set; }
        public DateTime Date { get; set; }
        public bool WeWon { get; set; }

        public virtual RivalTeam RivalTeam { get; set; }
        public virtual ICollection<DetailOfGamePlayed> DetailOfGamePlayeds { get; set; }
    }
}
