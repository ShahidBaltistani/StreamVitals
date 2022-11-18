using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Vu360Sol.ViewModel.Doctors;
using Vu360Sol.ViewModel.SalePersons;
using Vu360Sol.Web.DataSets;

namespace Vu360Sol.Web.Reports
{
    public class DataSetFill
    {
        StreamVitalsDataSet dataset = new StreamVitalsDataSet();
        public DataSetFill()
        {

        }
        
        public async Task<StreamVitalsDataSet> FillDoctorDataSet(IEnumerable<DoctorAssignedViewModel> doctors)
        {
             dataset = new StreamVitalsDataSet();
            await Task.Run(() =>
            {

                DataTable dt = dataset.Tables["DoctorTable"];
               
                foreach(var item in doctors)
                {
                    DataRow row = dt.NewRow();
                    row["Id"] = item.DoctorId;
                    row["FirstName"] = item.Doctor.User.FirstName;
                    row["LastName"] = item.Doctor.User.LastName;
                    row["Email"] = item.Doctor.User.Email;
                    row["PhoneNumber"] = item.Doctor.User.PhoneNumber;
                    row["NPI"] = item.Doctor.NPI;
                    row["ProviderType"] = item.Doctor.ProviderType;
                    row["PracticeName"] = item.Doctor.PracticeName;
                    row["LocationName"] = item.Doctor.LocationName;
                    row["LocationAddress"] = item.Doctor.LocationAddress;
                    row["LocationState"] = item.Doctor.LocationState;
                    row["DoctorStatus"] = item.Doctor.DoctorStatus;
                    if (item.Note != null)
                    {
                        row["Note"] = item.Note.Discription;
                        row["AssignedOn"] = item.Date + " " + item.Time;
                    }
                    
                    dataset.Tables["DoctorTable"].Rows.Add(row);
                }

            });
            return dataset;
        }

        public async Task<DataSet> FillDoctorDataSetForExcel(IEnumerable<DoctorAssignedViewModel> doctors,bool notesRequired)
        {
             DataSet dataSet = new DataSet();
            await Task.Run(() =>
            {

                DataTable DoctorTable = dataSet.Tables.Add("DoctorTable");
                //dataSet = dataset.Tables["DoctorTable"];
                //var DoctorTable = new DataTable("DoctorTable");
                DoctorTable.Columns.Add("First Name", typeof(string));
                DoctorTable.Columns.Add("Last Name", typeof(string));
                DoctorTable.Columns.Add("Email", typeof(string));
                DoctorTable.Columns.Add("Phone Number", typeof(string));
                DoctorTable.Columns.Add("NPI", typeof(string));
                DoctorTable.Columns.Add("Location Name", typeof(string));
                DoctorTable.Columns.Add("State", typeof(string));
                DoctorTable.Columns.Add("Practice Name", typeof(string));
                DoctorTable.Columns.Add("Practice ID", typeof(string));
                if (notesRequired)
                {
                    DoctorTable.Columns.Add("Note", typeof(string));
                    DoctorTable.Columns.Add("Note CreatedOn", typeof(string));
                }

                foreach (var item in doctors)
                {

                    DataRow row = DoctorTable.NewRow();
                    row["First Name"] = item.Doctor.User.FirstName;
                    row["Last Name"] = item.Doctor.User.LastName;
                    row["Email"] = item.Doctor.User.Email;
                    row["Phone Number"] = item.Doctor.User.PhoneNumber;
                    row["NPI"] = item.Doctor.NPI;
                    row["Location Name"] = item.Doctor.LocationName;
                    row["State"] = item.Doctor.LocationState;
                    if (item.Doctor.Practice != null)
                    {
                        row["Practice Name"] = item.Doctor.Practice.Name;
                        row["Practice ID"] = item.Doctor.Practice.Id;
                    }
                    if (item.Note != null)
                    {
                        row["Note"] = item.Note.Discription;
                        row["Note CreatedOn"] = item.Date + " " + item.Time;
                    }

                    DoctorTable.Rows.Add(row);
                }
                //dataSet.Tables.Add(DoctorTable);
            });
            return dataSet;
        }

    }
}