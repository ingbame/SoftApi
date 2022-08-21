using System;
using System.Collections.Generic;

namespace KodiaksApi.Data.DbModels
{
    public partial class PasswordsHistory
    {
        public long HistoryId { get; set; }
        public long UserId { get; set; }
        public string Password { get; set; }

        public virtual User User { get; set; }
    }
}
