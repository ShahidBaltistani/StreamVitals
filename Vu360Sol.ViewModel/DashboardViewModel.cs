using System;
using System.Collections.Generic;
using System.Text;
using Vu360Sol.ViewModel.Account;
using Vu360Sol.ViewModel.RequestDemoes;
using Vu360Sol.ViewModel.Visitors;

namespace Vu360Sol.ViewModel
{
   public class DashboardViewModel
    {
        public IEnumerable<RequestDemoViewModel> RequestDemoes { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }

    }
}
