using System;
using System.Collections.Generic;
using System.Text;
using Vu360Sol.ViewModel.Doctors;
using Vu360Sol.ViewModel.RequestDemoes;

namespace Vu360Sol.ViewModel.Notes
{
  public class NoteViewModel
    {

        public int Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public int ReferenceId { get; set; }
        public string Discription { get; set; }
        public int RefrenceTableId { get; set; }
        public RefrenceTableViewModel RefrenceTable { get; set; }
    }
}
