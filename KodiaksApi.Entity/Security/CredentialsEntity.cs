using KodiaksApi.Entity.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Entity.Security
{
    public class CredentialsEntity
    {
        public CredentialsEntity()
        {
            Member = new MemberEntity();
            User = new UserEntity();
        }
        public MemberEntity Member { get; set; }
        public UserEntity User { get; set; }
    }
}
