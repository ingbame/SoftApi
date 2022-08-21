using System;
using System.Collections.Generic;

namespace KodiaksApi.Data.DbModels
{
    public partial class AssignRoleMenu
    {
        public int RoleId { get; set; }
        public int MenuItemId { get; set; }

        public virtual MenuItem MenuItem { get; set; }
        public virtual Role Role { get; set; }
    }
}
