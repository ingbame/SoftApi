using System;
using System.Collections.Generic;

namespace KodiaksApi.Data.DbModels
{
    public partial class MenuItem
    {
        public int MenuItemId { get; set; }
        public string ItemKey { get; set; }
        public string IconSource { get; set; }
    }
}
