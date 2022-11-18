using System;
using System.Collections.Generic;
using System.Text;
using Vu360Sol.ViewModel.Account;

namespace Vu360Sol.ViewModel.SalePersons
{
   public class SalePersonViewModel
    {
        public int Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public string ImagePath { get; set; }
        public int UserId { get; set; }
        public virtual UserViewModel User { get; set; }

    }
}
