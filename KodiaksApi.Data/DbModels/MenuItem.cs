using System;
using System.Collections.Generic;

namespace SoftApi.Data.DbModels
{
    public partial class MenuItem
    {
        public int MenuItemId { get; set; }
        public string ItemKey { get; set; }
        public string IconSource { get; set; }
    }
}
