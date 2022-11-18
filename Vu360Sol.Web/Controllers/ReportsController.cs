using ClosedXML.Excel;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vu360Sol.ViewModel.Doctors;
using Vu360Sol.ViewModel.SalePersons;
using Vu360Sol.Web.DataSets;
using Vu360Sol.Web.Reports;

namespace Vu360Sol.Web.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult DoctorsReport()
        {
            return View();
        }

        public async Task<ActionResult> ExportDoctorsReport(string ReportOf, string ExportTo, bool NotesRequired = false)
        {

            string url = ReportOf == "con" ? "DoctorAssign/ContectedDoctorsReport/" : "DoctorAssign/AllDoctorsReport/";

            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync(url + NotesRequired);

                responseTask.Wait();
                //To store result of web api response.   
                var result = responseTask.Result;
                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<DoctorAssignedViewModel>>();
                    readTask.Wait();
                    var doctors = readTask.Result;
                    if (doctors.Count > 0)
                    {
                        if (ExportTo == "xls")
                        {
                            var doctorSet = await new DataSetFill().FillDoctorDataSetForExcel(doctors, NotesRequired);
                            var file = await ExportExcelFile(doctorSet);
                            if (file != "")
                            {
                                return Json(new { fileName = file, success = true, message = "Successfully Exported" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { fileName = "", success = false, message = "Face error while Export" }, JsonRequestBehavior.AllowGet);
                            }

                        }
                        else
                        {
                            var doctorSet = await new DataSetFill().FillDoctorDataSet(doctors);

                            var file = await ExportPDFFile(doctorSet);
                            if (file != "")
                            {
                                return Json(new { fileName = file, success = true, message = "Successfully Exported" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { fileName = "", success = false, message= "Face error while Export" }, JsonRequestBehavior.AllowGet);
                            }

                        }
                    }
                    else
                    {
                        return Json(new { fileName = "", success = false,message="No Matched Record Found for Export" }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    //Error response received   

                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            return  Json(new { fileName = "", success = false, message = "Face error while Export" }, JsonRequestBehavior.AllowGet);
        }
        public async Task<string> ExportExcelFile(DataSet doctorSet)
        {
            var response = "";
            var FileName = "DoctorList " + Utility.getDefaultTime().ToString("dd-MMM-yyyy hh-mm tt") + ".xlsx";
            string fullPath = Path.Combine(Server.MapPath("~/ExportReport"), FileName);
            bool exists = System.IO.Directory.Exists(Server.MapPath("~/ExportReport"));

            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath("~/ExportReport"));
       
            await Task.Run(() =>
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    DataTable DataTable;
                    DataTable = doctorSet.Tables["DoctorTable"].Copy();
                    wb.Worksheets.Add(doctorSet);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                        stream.WriteTo(file);
                        file.Close();
                        response = FileName;
                        
                        
                    }
                }
            });

            return response;
        }
        public async Task<string> ExportPDFFile(DataSet doctorSet)
        {
            var response = "";
            var FileName = "DoctorList " + Utility.getDefaultTime().ToString("dd-MMM-yyyy hh-mm tt") + ".pdf";
            string fullPath = Path.Combine(Server.MapPath("~/ExportReport"), FileName);
            bool exists = System.IO.Directory.Exists(Server.MapPath("~/ExportReport"));

            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath("~/ExportReport"));
            
            await Task.Run(() =>
            {
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reports"), "DoctorReport.rpt"));
                rd.SetDataSource(doctorSet);
                
                rd.ExportToDisk(ExportFormatType.PortableDocFormat, fullPath);
              
                        response = FileName;

                
            });

            return response;
        }
        [HttpGet]
        [DeleteFileAttribute] 
        public ActionResult Download(string file,string type)
        {
            
            string fullPath = Path.Combine(Server.MapPath("~/ExportReport"), file);
            if (System.IO.File.Exists(fullPath))
            {
                if (type == "xls")
                    return File(fullPath, "application/vnd.ms-excel", file);
                else
                    return File(fullPath, "application/pdf", file);
            }
            return null;
        }
    }
    public class DeleteFileAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Flush();

            string filePath = (filterContext.Result as FilePathResult).FileName;

            if (System.IO.File.Exists(filePath))
            System.IO.File.Delete(filePath);
        }
    }

}