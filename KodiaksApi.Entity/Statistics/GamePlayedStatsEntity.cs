using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftApi.Entity.Statistics
{
    public class GamePlayedStatsEntity
    {
        public GamePlayedStatsEntity()
        {
            Game = new GamePlayedEntity();
            DetailOfOurGame = new List<GamePlayedOurDetailEntity>();
            DetailOfTheRivalGame = new List<GamePlayedRivalDetailEntity>();
        }
        public GamePlayedEntity Game { get; set; }
        public List<GamePlayedOurDetailEntity> DetailOfOurGame { get; set; }
        public List<GamePlayedRivalDetailEntity> DetailOfTheRivalGame { get; set; }
    }
}
