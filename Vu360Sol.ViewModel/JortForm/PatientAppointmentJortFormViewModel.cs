using System;
using System.Collections.Generic;
using System.Text;

namespace Vu360Sol.ViewModel.JortForm
{
   public class PatientAppointmentJortFormViewModel
    {
        public string PracticeId { get; set; }
        public string ClinicId { get; set; }
        public string DoctorId { get; set; }

        public string PatientId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }

        public string ResidentialAddressSearch { get; set; }
        public string ResidentialAddress_Line1 { get; set; }
        public string ResidentialAddress_Line2 { get; set; }
        public string ResidentialCity { get; set; }
        public string ResidentialState { get; set; }
        public string ResidentialPostalCode { get; set; }
        public string ResidentialCountry { get; set; }

        public string ResidentialAddress
        {
            get
            {
                return ResidentialAddress_Line1 + " " +
                ResidentialAddress_Line2 + " " + ResidentialCity + " " + ResidentialState + " " +
                ResidentialCountry + " " + ResidentialPostalCode;
            }
        }
        public string SearchCountry { get; set; }

        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string ConsentTerms { get; set; }

        public string Event_Id { get; set; }

        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = false;
    }
}
