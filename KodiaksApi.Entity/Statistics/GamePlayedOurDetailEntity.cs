using SoftApi.Entity.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftApi.Entity.Statistics
{
    public class GamePlayedOurDetailEntity : GamePlayedDetailEntity
    {
        public GamePlayedOurDetailEntity()
        {
            Member = new MemberEntity();
        }
        public long? OurDetailId { get; set; }
        public long? MemberId { get; set; }
        public MemberEntity Member { get; set; }
    }
}
