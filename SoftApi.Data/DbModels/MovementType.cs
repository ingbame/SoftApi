using System;
using System.Collections.Generic;

namespace SoftApi.Data.DbModels
{
    public partial class MovementType
    {
        public MovementType()
        {
            Movements = new HashSet<Movement>();
        }

        public short MovementTypeId { get; set; }
        public string MovementTypeKey { get; set; }
        public string MovementTypeDesc { get; set; }

        public virtual ICollection<Movement> Movements { get; set; }
    }
}
