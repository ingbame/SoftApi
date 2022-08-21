using System;
using System.Collections.Generic;

namespace KodiaksApi.Data.DbModels
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            Bills = new HashSet<Bill>();
            Incomes = new HashSet<Income>();
        }

        public short MethodId { get; set; }
        public string MethodDesc { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Income> Incomes { get; set; }
    }
}
