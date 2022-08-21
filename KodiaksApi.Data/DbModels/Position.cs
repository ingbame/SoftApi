using System;
using System.Collections.Generic;

namespace KodiaksApi.Data.DbModels
{
    public partial class Position
    {
        public Position()
        {
            Rosters = new HashSet<Roster>();
        }

        public short PositionId { get; set; }
        public string KeyValue { get; set; }
        public string PositionDesc { get; set; }

        public virtual ICollection<Roster> Rosters { get; set; }
    }
}
