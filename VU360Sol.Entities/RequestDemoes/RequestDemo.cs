using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VU360Sol.Entities.Common;
using VU360Sol.Entities.Doctors;

namespace VU360Sol.Entities.RequestDemoes
{
   public class RequestDemo : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }
        public string Email { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public int? DoctorId { get; set; }
        public Doctor Doctor { get; set; }


    }
}
