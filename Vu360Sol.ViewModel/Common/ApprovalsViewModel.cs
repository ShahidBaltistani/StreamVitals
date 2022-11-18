using System;
using System.Collections.Generic;
using System.Text;
using Vu360Sol.ViewModel.Doctors;
using Vu360Sol.ViewModel.SalePersons;

namespace Vu360Sol.ViewModel.Common
{
    public class ApprovalsViewModel
    {
        public IEnumerable<DoctorViewModel> Doctors { get; set; }
        public IEnumerable<SalePersonViewModel> SalePersons { get; set; }
    }
}
