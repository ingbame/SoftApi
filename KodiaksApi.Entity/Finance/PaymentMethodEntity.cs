using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Entity.Finance
{
    public class PaymentMethodEntity
    {
        public short? MethodId { get; set; }
        public string MethodDesc { get; set; }
    }
}
