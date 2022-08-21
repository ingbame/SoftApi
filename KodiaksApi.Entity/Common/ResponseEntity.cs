using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Entity.Common
{
    public class ResponseEntity<TModel> where TModel : class
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public TModel Model { get; set; }
    }
}
