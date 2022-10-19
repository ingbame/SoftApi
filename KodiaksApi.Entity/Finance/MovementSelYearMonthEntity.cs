using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Entity.Finance
{
    public class MovementSelYearMonthEntity
    {
		public short MovementTypeId { get; set; }
		public string MovementTypeKey { get; set; }
		public string MovementTypeDesc { get; set; }
		public string MovementDate { get; set; }
		public decimal? Amount { get; set; }
	}
}
