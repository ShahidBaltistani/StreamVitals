using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Vu360Sol.ViewModel.JortForm;

namespace Vu360Sol.Web.SVRPDJortForm
{
    public class PatientAppointmentFormData
    {
        SqlConnection conn;
        SqlCommand comm;
        SqlDataReader dreader;
        protected string ConnectionString = "Data Source=34.73.221.148;Initial Catalog=SVRPDFormDB; user id=sa;password=360VUSolutions";
        public List<PatientAppointmentJortFormViewModel> getPaitents()
        {
            try
            {
                var list = new List<PatientAppointmentJortFormViewModel>();
                conn = new SqlConnection(ConnectionString);
                //conn = new SqlConnection(connstring);
                conn.Open();
                // Guid.Parse(paitentId.ToString())
                comm = new SqlCommand("select * from PatientAppointmentJortForm ", conn);
                try
                {
                    dreader = comm.ExecuteReader();
                    if (dreader.HasRows)
                    {
                        while (dreader.Read())
                        {

                            var model = new PatientAppointmentJortFormViewModel
                            {
                                ClinicId = dreader["ClinicId"].ToString(),
                                DoctorId = dreader["DoctorId"].ToString(),
                                PracticeId = dreader["PracticeId"].ToString(),
                                PatientId = dreader["PatientId"].ToString(),
                                ConsentTerms = dreader["ConsentTerms"].ToString(),
                                Event_Id = dreader["Event_Id"].ToString(),
                                FirstName = dreader["FirstName"].ToString(),
                                LastName = dreader["LastName"].ToString(),
                                Email = dreader["Email"].ToString(),
                                MobilePhone = dreader["MobilePhone"].ToString(),
                                ResidentialAddressSearch = dreader["ResidentialAddressSearch"].ToString(),
                                ResidentialAddress_Line1 = dreader["ResidentialAddress_Line1"].ToString(),
                                ResidentialAddress_Line2 = dreader["ResidentialAddress_Line2"].ToString(),
                                ResidentialCity = dreader["ResidentialCity"].ToString(),
                                ResidentialCountry = dreader["ResidentialCountry"].ToString(),
                                ResidentialPostalCode = dreader["ResidentialPostalCode"].ToString(),
                                ResidentialState = dreader["ResidentialState"].ToString(),
                                SearchCountry = dreader["SearchCountry"].ToString(),
                                CreatedOn = Convert.ToDateTime(dreader["CreatedOn"].ToString())



                            };
                            list.Add(model);
                        }
                    }
                    else
                    {
                        return null;
                    }
                    dreader.Close();
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    conn.Close();
                    //  model = null;
                }
                return list;

            }
            catch(Exception ex)
            {
                return null;
            }
            
            
        }
    }
}