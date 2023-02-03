using SoftApi.Entity.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftApi.Entity.Statistics
{
    public class GamePlayedRivalDetailEntity : GamePlayedDetailEntity
    {
        public GamePlayedRivalDetailEntity()
        {
            Member = new OpposingTeamMembers();
        }
        public long? RivalDetailId { get; set; }
        public int? MemberId { get; set; }
        public OpposingTeamMembers Member { get; set; }
    }
}
