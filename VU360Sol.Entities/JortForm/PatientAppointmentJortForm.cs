using System;
using System.Collections.Generic;
using System.Text;
using VU360Sol.Entities.Common;

namespace VU360Sol.Entities.JortForm
{
    public class PatientAppointmentJortForm: BaseEntity
    {
        public string PracticeId { get; set; }
        public string ClinicId { get; set; }
        public string DoctorId { get; set; }

        public string PatientId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
       
        public string ResidentialAddressSearch { get; set; }
        public string ResidentialAddress_Line1 { get; set; }
        public string ResidentialAddress_Line2 { get; set; }
        public string ResidentialCity { get; set; }
        public string ResidentialState { get; set; }
        public string ResidentialPostalCode { get; set; }
        public string ResidentialCountry { get; set; }

       
        public string SearchCountry { get; set; }

        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string ConsentTerms { get; set; }

        public string Event_Id { get; set; }
    }
}
