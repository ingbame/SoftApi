using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Entity.Finance
{
    public class MovementTypeEntity
    {
        public short? MovementTypeId { get; set; }
        public string MovementTypeKey { get; set; }
        public string MovementTypeDesc { get; set; }
    }
}
