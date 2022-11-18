using System;
using System.Collections.Generic;
using System.Text;

namespace Vu360Sol.ViewModel.Practices
{
   public class PracticeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
}
