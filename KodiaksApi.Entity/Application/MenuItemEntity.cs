using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Entity.Application
{
    public class MenuItemEntity
    {
        public int? MenuItemId { get; set; }
        public string Title { get; set; }
        public string IconSource { get; set; }
        public string TargetPage { get; set; }
    }
}
