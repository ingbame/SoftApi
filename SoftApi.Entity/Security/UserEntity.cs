using SoftApi.Entity.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftApi.Entity.Security
{
    public class UserEntity
    {
        public UserEntity()
        {
            RoleEn = new RoleEntity();
        }
        public long? UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public int? RoleId { get; set; }
        public bool? CanEdit { get; set; }
        public bool? SavePasswords { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public RoleEntity RoleEn { get; set; }
    }
}
