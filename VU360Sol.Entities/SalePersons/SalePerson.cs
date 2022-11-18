using System;
using System.Collections.Generic;
using System.Text;
using VU360Sol.Entities.Account;
using VU360Sol.Entities.Common;

namespace VU360Sol.Entities.SalePersons
{
   public class SalePerson : BaseEntity
    {
        public string ImagePath { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }


    }
}
