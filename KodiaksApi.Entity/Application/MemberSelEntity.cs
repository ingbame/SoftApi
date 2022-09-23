using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Entity.Application
{
    public class MemberSelEntity
    {
        public long? MemberId { get; set; }
		public long? UserId { get; set; }
        public int? RoleId { get; set; }
        public string RoleDesc { get; set; }
        public string FullName { get; set; }
        public string NickName { get; set; }
        public int? ShirtNumber { get; set; }
        public short? BTSideId { get; set; }
        public string BTSideDesc { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string CellPhoneNumber { get; set; }
        public bool? CanEdit { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
