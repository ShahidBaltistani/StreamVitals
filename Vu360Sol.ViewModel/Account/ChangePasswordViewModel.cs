using System;
using System.Collections.Generic;
using System.Text;

namespace Vu360Sol.ViewModel.Account
{
   public class ChangePasswordViewModel
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string NewPassword { get; set; }

    }
}
