using System;
using System.Collections.Generic;
using System.Text;
using Vu360Sol.ViewModel.Account;
using Vu360Sol.ViewModel.Practices;

namespace Vu360Sol.ViewModel.Doctors
{
   public class DoctorViewModel
    {
        public int Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public string LocationName { get; set; }
        public string ImagePath { get; set; }
        public string ProviderType { get; set; }
        public int UserId { get; set; }
        public virtual UserViewModel User { get; set; }
        public int StatusId { get; set; }
        public virtual StatusViewModel Status { get; set; }

        public string PracticeName { get; set; }
        public string LocationAddress { get; set; }

        public string WebsiteURL { get; set; }

        public string Type { get; set; }
        public string Credentials { get; set; }
        public string NPI { get; set; }
        public string Networks { get; set; }
        public string LocationCity { get; set; }
        public string LocationState { get; set; }
        public string LocationZip { get; set; }
        public string LocationCode { get; set; }
        public int? AppointmentCount { get; set; }
        public string DoctorStatus { get; set; }

        public int? PracticeId { get; set; }

        public virtual PracticeViewModel Practice { get; set; }


    }
}
