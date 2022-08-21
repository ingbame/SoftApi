using System;
using System.Collections.Generic;

namespace KodiaksApi.Data.DbModels
{
    public partial class Roster
    {
        public long RosterId { get; set; }
        public long MemberId { get; set; }
        public short PositionId { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual Position Position { get; set; }
    }
}
