using KodiaksApi.Entity.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiaksApi.Entity.Security
{
    public class CredentialsDtoEntity
    {
        public CredentialsDtoEntity()
        {
            Member = new MemberEntity();
            User = new LoginDtoEntity();
        }
        public MemberEntity Member { get; set; }
        public LoginDtoEntity User { get; set; }
    }
}
