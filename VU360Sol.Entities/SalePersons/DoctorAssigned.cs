using System;
using System.Collections.Generic;
using System.Text;
using VU360Sol.Entities.Doctors;
using VU360Sol.Entities.Notes;

namespace VU360Sol.Entities.SalePersons
{
   public class DoctorAssigned
    {
        public int Id { get; set; }
        public string AssignedBy { get; set; }
        public DateTime AssignedDate { get; set; }
        public int? DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public int? SalePersonId { get; set; }
        public SalePerson SalePerson { get; set; }
        public int? NoteId { get; set; }
        public Note Note { get; set; }
        public bool IsDeleted { get; set; }

    }
}
