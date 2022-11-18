using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace VU360Sol.Entities.Visitors
{
    public class Visitor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
       
        public int VisitorPurposeId { get; set; }
        public VisitorPurpose VisitorPurpose { get; set; }
        public DateTime CreatedOn { get; set; }
        //public int? SentEmailId { get; set; }
        //public SentEmail SentEmail { get; set; }

        public string Practice { get; set; }
    }
}
