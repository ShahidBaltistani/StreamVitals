using System;
using System.Collections.Generic;
using System.Text;
using Vu360Sol.ViewModel.Doctors;

namespace Vu360Sol.ViewModel.RequestDemoes
{
   public class RequestDemoViewModel
    {
        public int Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string Email { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public int? DoctorId { get; set; }
        public DoctorViewModel Doctor { get; set; }
        public string Dates
        {
            get
            {
                return ((DateTime)this.Date).ToString("dd-MMM-yyyy");
            }
        }
    }
}
