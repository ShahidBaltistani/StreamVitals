using System;
using System.Collections.Generic;
using System.Text;
using VU360Sol.Entities.Account;
using VU360Sol.Entities.Common;

namespace VU360Sol.Entities.Doctors
{
   public class Doctor :BaseEntity
    {
        public string LocationName { get; set; }
        public string ImagePath { get; set; }
        public string ProviderType { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

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

        public virtual Practice Practice { get; set; }


    }
}
