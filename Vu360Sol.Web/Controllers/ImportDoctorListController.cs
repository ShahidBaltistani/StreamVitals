using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vu360Sol.ViewModel.Account;
using Vu360Sol.ViewModel.Doctors;
using Excel = Microsoft.Office.Interop.Excel;

namespace Vu360Sol.Web.Controllers
{
    [Authorize]
    public class ImportDoctorListController : Controller
    {
        public ActionResult ExportFile()
        {
            return View();
        }

        //Download Excel file already stored at location 
        public ActionResult DownloadFileFromFolder()
        {
            string fileName = "DoctorImportTemplate.xlsx";
            string fullName = Server.MapPath("~/ExcelTemplate/" + fileName);
            byte[] fileBytes = GetFile(fullName);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        byte[] GetFile(string s)
        {
            FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }


        //Import Excel file into project and add in table
        //[HttpPost]
        //public ActionResult ImportOld(HttpPostedFileBase excelfile)
        //{
        //    if (excelfile == null || excelfile.ContentLength == 0)
        //    {
        //        ViewBag.Error = "Please Select Excel File <br>";
        //        return View("ExportFile");
        //    }
        //    else
        //    {
        //        if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
        //        {
        //            //create new excel file with other name 
        //            string filename = excelfile.FileName.ToLower().ToString();
        //            string[] exts = filename.Split('.');
        //            string name = exts[0].ToString() + Utility.getDefaultTime().ToString("yyyyMMddHHmmss");//DateTime.Now.ToString("yyyyMMddHHmmss")
        //            string ext = exts[1].ToString();
        //            string FinalResult = name + "." + ext;

        //            //save file at path
        //            string path = Server.MapPath("~/excelfolder/" + FinalResult);
        //            if (System.IO.File.Exists(path))
        //                System.IO.File.Delete(path);
        //            excelfile.SaveAs(path);
        //            //read data from excel file
        //            Excel.Application application = new Excel.Application();
        //            Excel.Workbook workbook = application.Workbooks.Open(path);
        //            Excel.Worksheet worksheet = workbook.ActiveSheet;
        //            Excel.Range range = worksheet.UsedRange;

        //            List<DoctorViewModel> listDoctors = new List<DoctorViewModel>();
        //            for (int row = 2; row <= range.Rows.Count; row++)
        //            {
        //                DoctorViewModel p = new DoctorViewModel();
        //                p.User = new UserViewModel();
                        
        //                p.User.FirstName = ((Excel.Range)range.Cells[row, 1]).Text;
        //                p.User.LastName = ((Excel.Range)range.Cells[row, 2]).Text;
        //                p.User.Email = ((Excel.Range)range.Cells[row, 3]).Text;
        //                p.User.PhoneNumber = ((Excel.Range)range.Cells[row, 4]).Text;
        //                p.Type= ((Excel.Range)range.Cells[row, 5]).Text;
        //                p.ProviderType = ((Excel.Range)range.Cells[row, 6]).Text;
        //                p.Credentials = ((Excel.Range)range.Cells[row, 7]).Text;
        //                p.NPI = ((Excel.Range)range.Cells[row, 8]).Text;
        //                p.Networks = ((Excel.Range)range.Cells[row, 9]).Text;
        //                p.PracticeName = ((Excel.Range)range.Cells[row, 10]).Text;
        //                p.LocationName = ((Excel.Range)range.Cells[row, 11]).Text;
        //                p.LocationAddress = ((Excel.Range)range.Cells[row, 12]).Text;
        //                p.LocationCity = ((Excel.Range)range.Cells[row, 13]).Text;
        //                p.LocationState = ((Excel.Range)range.Cells[row, 14]).Text;
        //                p.LocationZip = ((Excel.Range)range.Cells[row, 15]).Text;
        //                p.LocationCode = ((Excel.Range)range.Cells[row, 16]).Text;
        //                p.AppointmentCount = int.Parse(((Excel.Range)range.Cells[row, 17]).Text);
        //                p.DoctorStatus = ((Excel.Range)range.Cells[row, 18]).Text;
        //                //p.WebsiteURL = ((Excel.Range)range.Cells[row, 10]).Text;

        //                listDoctors.Add(p);

        //            }
        //            ViewBag.ListDoctors = listDoctors;
        //            return View("ExcelFileData");
        //        }
        //        else
        //        {
        //            ViewBag.Error = "File type is incorrect, Please select Excel file. <br>";
        //            return View("ExportFile");
        //        }
        //    }

        //}


        public ActionResult ExcelFileData()
        {
            return View();
        }

        /// me
        [HttpPost]
        public ActionResult Import(HttpPostedFileBase excelfile)
        {

            DataTable dt = new DataTable();
            if (excelfile != null && excelfile.ContentLength > 0 && System.IO.Path.GetExtension(excelfile.FileName).ToLower() == ".xlsx")
            {
                string path = Path.Combine(Server.MapPath("~/excelfolder"), Path.GetFileName(excelfile.FileName));
                excelfile.SaveAs(path);
                using (XLWorkbook workbook = new XLWorkbook(path))
                {
                    IXLWorksheet worksheet = workbook.Worksheet(1);
                    bool FirstRow = true;
                    string readRange = "1:1";
                    foreach (IXLRow row in worksheet.RowsUsed())
                    {
                        if (FirstRow)
                        {
                            readRange = string.Format("{0}:{1}", 1, row.LastCellUsed().Address.ColumnNumber);
                            foreach (IXLCell cell in row.Cells(readRange))
                            {
                                dt.Columns.Add(cell.Value.ToString());
                            }
                            FirstRow = false;
                        }
                        else
                        {
                            dt.Rows.Add();
                            int cellIndex = 0;
                            foreach (IXLCell cell in row.Cells(readRange))
                            {
                                dt.Rows[dt.Rows.Count - 1][cellIndex] = cell.Value;
                                cellIndex++;
                            }
                        }
                    }
                    if (FirstRow)
                    {
                        ViewBag.Message = "Empty Excel File!";
                    }
                }
            }
            else
            {
                ViewBag.Message = "Please select file with .xlsx extension!";
            }
            List<DoctorViewModel> listDoctors = new List<DoctorViewModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DoctorViewModel p = new DoctorViewModel();
                p.User = new UserViewModel();

                p.User.FirstName = dt.Rows[i]["FirstName"].ToString();
                p.User.LastName = dt.Rows[i]["LastName"].ToString();
                p.User.Email =   dt.Rows[i]["Email"].ToString();
                p.User.PhoneNumber = dt.Rows[i]["PhoneNumber"].ToString();
                p.Type = dt.Rows[i]["Type"].ToString();
                p.ProviderType = dt.Rows[i]["ProviderType"].ToString();
                p.Credentials = dt.Rows[i]["Credentials"].ToString();
                p.NPI = dt.Rows[i]["NPI"].ToString();
                p.Networks = dt.Rows[i]["Networks"].ToString();
                p.PracticeName = dt.Rows[i]["PracticeName"].ToString();
                p.LocationName = dt.Rows[i]["LocationName"].ToString();
                p.LocationAddress = dt.Rows[i]["LocationAddress"].ToString();
                p.LocationCity = dt.Rows[i]["LocationCity"].ToString();
                p.LocationState = dt.Rows[i]["LocationState"].ToString();
                p.LocationZip = dt.Rows[i]["LocationZip"].ToString();
                p.LocationCode = dt.Rows[i]["LocationCode"].ToString();
                p.AppointmentCount = int.Parse(dt.Rows[i]["AppointmentCount"].ToString());
                p.DoctorStatus = dt.Rows[i]["DoctorStatus"].ToString();
                
                listDoctors.Add(p);
            }
            ViewBag.ListDoctors = listDoctors;
            return View("ExcelFileData");
        }

    }
}