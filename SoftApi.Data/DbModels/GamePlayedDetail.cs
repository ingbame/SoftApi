using System;
using System.Collections.Generic;

namespace SoftApi.Data.DbModels
{
    public partial class GamePlayedDetail
    {
        public GamePlayedDetail()
        {
            DetailOfOurGamePlayeds = new HashSet<DetailOfOurGamePlayed>();
            DetailOfTheRivalGamePlayeds = new HashSet<DetailOfTheRivalGamePlayed>();
        }

        public long DetailId { get; set; }
        public int GameId { get; set; }
        public int PositionAtBat { get; set; }
        public bool IsRun { get; set; }
        public bool IsHit { get; set; }
        public bool IsDouble { get; set; }
        public bool IsTriple { get; set; }
        public bool IsHomeRun { get; set; }
        public int RunsBattedIn { get; set; }
        public int Walks { get; set; }
        public int StrikeOut { get; set; }
        public int StolenBases { get; set; }
        public int CaughtStealing { get; set; }
        public bool IsOut { get; set; }
        public int? OutValue { get; set; }
        public int? OutSector { get; set; }
        public string CenterValue { get; set; }
        public bool IsPitcher { get; set; }

        public virtual GamesPlayed Game { get; set; }
        public virtual ICollection<DetailOfOurGamePlayed> DetailOfOurGamePlayeds { get; set; }
        public virtual ICollection<DetailOfTheRivalGamePlayed> DetailOfTheRivalGamePlayeds { get; set; }
    }
}
