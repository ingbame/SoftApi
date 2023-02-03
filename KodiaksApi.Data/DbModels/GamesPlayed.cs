using System;
using System.Collections.Generic;

namespace SoftApi.Data.DbModels
{
    public partial class GamesPlayed
    {
        public GamesPlayed()
        {
            DetailOfOurGamePlayeds = new HashSet<DetailOfOurGamePlayed>();
            DetailOfTheRivalGamePlayeds = new HashSet<DetailOfTheRivalGamePlayed>();
        }

        public int GameId { get; set; }
        public int RivalTeamId { get; set; }
        public DateTime Date { get; set; }
        public bool WeWon { get; set; }

        public virtual RivalTeam RivalTeam { get; set; }
        public virtual ICollection<DetailOfOurGamePlayed> DetailOfOurGamePlayeds { get; set; }
        public virtual ICollection<DetailOfTheRivalGamePlayed> DetailOfTheRivalGamePlayeds { get; set; }
    }
}
