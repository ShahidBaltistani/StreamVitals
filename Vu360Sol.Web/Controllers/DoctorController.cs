using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vu360Sol.ViewModel.Account;
using Vu360Sol.ViewModel.Doctors;
using Vu360Sol.ViewModel.Practices;
using Vu360Sol.ViewModel.RequestDemoes;
using Vu360Sol.ViewModel.SharedViewModels;

namespace Vu360Sol.Web.Controllers
{
    [Authorize]
    public class DoctorController : Controller
    {
        public ActionResult DoctorList()
        {
            return View();
        }
        public ActionResult DoctorListPage(int PageNo = 1, string Search = "")
        {
            ViewBag.Register = false;
            var data = new { PageNo, Search, PageSize = Utility.PageSize };
            var SerializeObject = JsonConvert.SerializeObject(data);
            DoctorViewModelPaginationModel pageModel = new DoctorViewModelPaginationModel();

            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(SerializeObject);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                var responseTask = client.PostAsync("doctor/getall", byteContent);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<DoctorViewModelPaginationModel>();
                    readTask.Wait();
                    pageModel = readTask.Result;
                    pageModel.page = new Pager(pageModel.Count, PageNo, Utility.PageSize);
                    pageModel.SearchTerm = Search;
                    ModelState.Clear();
                    return PartialView("_DoctorPartial", pageModel);
                }
                else
                {
                    //Error response received   
                    pageModel.doctorViewModels = Enumerable.Empty<DoctorViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return PartialView("_DoctorPartial", pageModel);
        }

       
        
      
        public ActionResult CreateDoctor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDoctor(DoctorViewModel doctor, HttpPostedFileBase file)
        {

            using (var client = Utility.GetHttpClient())
            {
                string FinalResult = null;
                if (file != null)
                {
                    string[] exts = file.FileName.Split('.');
                    string names = exts[0].ToString() + Utility.getDefaultTime().ToString("yyyyMMddHHmmss");//DateTime.Now.ToString("yyyyMMddHHmmss")
                    string ext = exts[1].ToString();
                    FinalResult = names + "." + ext;
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles/"), Path.GetFileName(FinalResult));
                    file.SaveAs(path);
                }
                int UserID = Convert.ToInt32(Session["UserId"]);
                DoctorViewModel model = new DoctorViewModel
                {
                    ImagePath = FinalResult,
                    User = new UserViewModel
                    {
                        FirstName = doctor.User.FirstName,
                        LastName = doctor.User.LastName,

                        Email = doctor.User.Email,
                        PhoneNumber = doctor.User.PhoneNumber,
                        GenderId = doctor.User.GenderId,
                        IsActive = false,
                        RoleId = 2,
                        CreatedOn =Utility.getDefaultTime(),
                        CreatedBy = UserID,

                    },
                    LocationName = doctor.LocationName,
                    ProviderType = doctor.ProviderType,
                    PracticeName = doctor.PracticeName,
                    LocationAddress = doctor.LocationAddress,
                    NPI=doctor.NPI,
                    LocationState=doctor.LocationState,
                    WebsiteURL = doctor.WebsiteURL,
                    CreatedBy = UserID,
                    CreatedOn = Utility.getDefaultTime(), //DateTime.Now
                    StatusId = 1,
                    IsActive = true
                };
                var myContent = JsonConvert.SerializeObject(model);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                var responseTask = client.PostAsync("doctor/create/", byteContent);
                responseTask.Wait();
                var result = responseTask.Result;
                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("DoctorList");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }


            return View("DoctorList");
        }


        [HttpPost]
        public ActionResult DoctorsListSave(List<DoctorViewModel> doctor)
        {

            using (var client = Utility.GetHttpClient())
            {
                var doctorList = new List<DoctorViewModel>();
                foreach (var item in doctor)
                {
                    int UserID = Convert.ToInt32(Session["UserId"]);
                    DoctorViewModel model = new DoctorViewModel();
                    model.User = new UserViewModel();
                    model.User.FirstName = item.User.FirstName;
                    model.User.LastName = item.User.LastName;
                    model.User.Email = item.User.Email;
                    model.User.PhoneNumber = item.User.PhoneNumber;
                    model.Type = item.Type;
                    model.ProviderType = item.ProviderType;
                    model.Credentials = item.Credentials;
                    model.NPI = item.NPI;
                    model.Networks = item.Networks;
                    model.PracticeName = item.PracticeName;
                    model.LocationName = item.LocationName;
                    model.LocationAddress = item.LocationAddress;
                    model.LocationCity = item.LocationCity;
                    model.LocationState = item.LocationState;
                    model.LocationZip = item.LocationZip;
                    model.LocationCode = item.LocationCode;
                    model.AppointmentCount = item.AppointmentCount;
                    model.DoctorStatus = item.DoctorStatus;
                    model.User.GenderId = item.User.GenderId;

                    model.CreatedBy = UserID;
                    model.CreatedOn = Utility.getDefaultTime();
                    model.UserId = UserID;
                    model.StatusId = 1;
                    model.IsActive = true;
                    model.User.RoleId = 2;
                    doctorList.Add(model);
                }
                    var myContent = JsonConvert.SerializeObject(doctorList);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                    var responseTask = client.PostAsync("doctor/ImportDoctors", new StringContent(myContent, System.Text.Encoding.UTF8, "application/Json"));
                    responseTask.Wait();
                    var result = responseTask.Result;
                    ////If success received   
                    if (!result.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }

                
                
            }

            return Json(true);
        }


        [HttpPost]
        public async Task<ActionResult> Schedule(RequestDemoViewModel model, string Date)
        {
            //model.Date = Convert.ToDateTime(DateTime.ParseExact(Date, "MM/dd/yyyy", CultureInfo.InvariantCulture));
            using (var client = Utility.GetHttpClient())
            {
                bool IsUserRegisterd = false;
                if (model.Doctor != null && model.Doctor.User != null)
                {
                    IsUserRegisterd = model.Doctor.User.IsActive;
                   // model.Doctor = null;
                }
                
                var myContent = JsonConvert.SerializeObject(model);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                var responseTask = client.PostAsync("requestdemo/CreateDoctorRequest/", byteContent);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //model.Doctor = new DoctorViewModel();
                    //model.Doctor.User = new UserViewModel
                    //{
                    //    Email = model.Email,
                    //    FirstName = model.FirstName,
                    //    LastName = model.LastName,
                    //};
                   
                   
                      var  email = await new Email().AppointmentAndRegistrationLink(model, IsUserRegisterd);
                    
                    
                    if (email)
                    {
                        return Json(new { success = true, responseText = "Email Sent Successfully. Thank you for contacting us." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, responseText = "Face issue while sending email. Please try later." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult UpdateDoctor(int Id)
        {
            DoctorViewModel employee;
            List<PracticeViewModel> practice;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("Doctor/GetById/" + Id + "");
                responseTask.Wait();
                var result = responseTask.Result;
                var readTask = result.Content.ReadAsAsync<DoctorViewModel>();
                readTask.Wait();
                employee = readTask.Result;

                var response = client.GetAsync("Practice/GetAllPractice/");
                response.Wait();
                var result2 = response.Result;
                var readTask2 = result2.Content.ReadAsAsync<List<PracticeViewModel>>();
                readTask2.Wait();
                practice = readTask2.Result;
            }


            Session["image"] = employee.ImagePath;
            ViewBag.drList = employee;
            ViewBag.PracticeList = practice;
            TempData["DoctorUpdate"] = employee;
            return View(employee);
        }
        [HttpPost]
        public ActionResult UpdateDoctor(DoctorViewModel doctor, HttpPostedFileBase file)
        {
            if (file == null)
            {
                doctor.ImagePath = Session["image"].ToString();
            }
            else
            {
                string filename = file.FileName.ToLower().ToString();
                string[] exts = filename.Split('.');
                string name = exts[0].ToString() + Utility.getDefaultTime().ToString("yyyyMMddHHmmss");//DateTime.Now.ToString("yyyyMMddHHmmss")
                string ext = exts[1].ToString();
                string FinalResult = name + "." + ext;
                string _ImagesPath = "~/UploadedFiles/";

                // Save New File....
                string path = Path.Combine(Server.MapPath(_ImagesPath), Path.GetFileName(FinalResult));
                file.SaveAs(path);

                doctor.ImagePath = FinalResult;

            }
            using (var client = Utility.GetHttpClient())
            {
                var model = new DoctorViewModel();
                var responseT = client.GetAsync("doctor/getbyid/" + doctor.Id + "");
                responseT.Wait();
                var dr = responseT.Result.Content.ReadAsAsync<DoctorViewModel>();
                model = dr.Result;
                if (model != null)
                {
                    doctor.CreatedOn = model.CreatedOn == null ? DateTime.Now : model.CreatedOn;
                    doctor.User.CreatedOn = model.User.CreatedOn == null ? DateTime.Now : model.User.CreatedOn;
                    doctor.CreatedBy = model.CreatedBy == null ? 1 : model.User.CreatedBy;
                    doctor.User.CreatedBy = model.User.CreatedBy == null ? 1 : model.User.CreatedBy;
                }
                var doc = TempData["DoctorUpdate"] as DoctorViewModel;
                int UserID = Convert.ToInt32(Session["UserId"]);
                doctor.User.RoleId = 2;
                doctor.User.IsActive = doctor.User.IsActive;
                doctor.StatusId = 1;
                doctor.IsActive = true;
                doctor.ModifiedBy = UserID == 0 ? 0 : UserID;
                doctor.User.ModifiedBy = UserID == 0 ? 0 : UserID; 
                doctor.ModifiedOn = Utility.getDefaultTime();
              
                doctor.User.ModifiedOn = Utility.getDefaultTime();

                var myContent = JsonConvert.SerializeObject(doctor);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                var responseTask = client.PostAsync("doctor/update/", byteContent);
                responseTask.Wait();
                var result = responseTask.Result;
                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("DoctorList");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }


            return View("DoctorList");
        }

        public JsonResult DeleteDoctor(int id)
        {
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("doctor/delete/" + id + "");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return Json(true);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return Json(true);
        }

        [HttpPost]
        public async Task<ActionResult> SendRegistrationEmail(DoctorViewModel model)
        {
            var email = await new Email().doctorRegistrationEmail(model);
            if (email)
            {
                return Json(new { success = true, responseText = "Email Sent Successfully." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, responseText = "Face issue while sending email. Please try later." }, JsonRequestBehavior.AllowGet);
            }

          
        }
    }
}