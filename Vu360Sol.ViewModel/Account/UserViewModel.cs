using System;
using System.Collections.Generic;
using System.Text;
using Vu360Sol.ViewModel.Common;
using Vu360Sol.ViewModel.SalePersons;

namespace Vu360Sol.ViewModel.Account
{
   public class UserViewModel
    {
        public int Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public virtual RoleViewModel Role { get; set; }
        public int GenderId { get; set; }
        public virtual GenderViewModel Gender { get; set; }

    }
}
