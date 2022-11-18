using System;
using System.Collections.Generic;
using System.Text;
using Vu360Sol.ViewModel.Doctors;
using Vu360Sol.ViewModel.Notes;
using Vu360Sol.ViewModel.SharedViewModels;

namespace Vu360Sol.ViewModel.SalePersons
{
   public class DoctorAssignedViewModel
    {
        public int Id { get; set; }
        public string AssignedBy { get; set; }
        public DateTime AssignedDate { get; set; }
        public int? DoctorId { get; set; }
        public DoctorViewModel Doctor { get; set; }
        public int? SalePersonId { get; set; }
        public SalePersonViewModel SalePerson { get; set; }
        public int? NoteId { get; set; }
        public NoteViewModel Note { get; set; }
        public Pager Pager { get; set; }
        public string SearchTerm { get; set; }
        public bool IsDeleted { get; set; }


        public string Date
        {
            get
            {
                return ((DateTime)this.AssignedDate).ToString("dd-MMM-yyyy");
            }
        }
        public string Time
        {
            get
            {
                return ((DateTime)this.AssignedDate).ToString("hh:mm tt");
            }
        }
    }
}
