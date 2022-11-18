using System;
using System.Collections.Generic;
using System.Text;

namespace Vu360Sol.ViewModel.Visitors
{
    public class VisitorViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public int VisitorPurposeId { get; set; }
        public VisitorPurposeViewModel VisitorPurpose { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Practice { get; set; }
    }
}
