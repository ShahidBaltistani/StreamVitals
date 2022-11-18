using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VU360Sol.Entities.Common;
using VU360Sol.Entities.SalePersons;

namespace VU360Sol.Entities.Account
{
   public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; } 
        public int GenderId { get; set; }
        public virtual Gender Gender { get; set; }
    }
}
