using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftApi.Entity.Statistics
{
    public class GamePlayedEntity
    {
        public GamePlayedEntity()
        {
            RivalData = new RivalTeamEntity();
        }
        public int? GameId { get; set; }
        public int? RivalTeamId { get; set; }
        public RivalTeamEntity RivalData { get; set; }
        public DateTime? Date { get; set; }
        public bool? WeWon { get; set; }
    }
}
