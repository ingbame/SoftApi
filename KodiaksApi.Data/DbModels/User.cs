﻿using System;
using System.Collections.Generic;

namespace SoftApi.Data.DbModels
{
    public partial class User
    {
        public User()
        {
            Members = new HashSet<Member>();
            Movements = new HashSet<Movement>();
            PasswordsHistories = new HashSet<PasswordsHistory>();
            Rosters = new HashSet<Roster>();
        }

        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public int RoleId { get; set; }
        public bool CanEdit { get; set; }
        public bool SavePasswords { get; set; }
        public bool IsVerified { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Member> Members { get; set; }
        public virtual ICollection<Movement> Movements { get; set; }
        public virtual ICollection<PasswordsHistory> PasswordsHistories { get; set; }
        public virtual ICollection<Roster> Rosters { get; set; }
    }
}
