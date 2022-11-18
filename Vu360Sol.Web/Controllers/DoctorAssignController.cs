using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Vu360Sol.ViewModel.Doctors;
using Vu360Sol.ViewModel.SalePersons;
using Vu360Sol.ViewModel.SharedViewModels;

namespace Vu360Sol.Web.Controllers
{
    [Authorize]
    public class DoctorAssignController : Controller
    {
        // GET: DoctorAssign
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("DoctorAssign/delete/" + id + "");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return Json("True");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return Json("True");
        }

        public ActionResult GetByDoctorId(int id)
        {
            IEnumerable<DoctorAssignedViewModel> doctors = null;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("DoctorAssign/GetByDoctorId/" + id + "");
                responseTask.Wait();
                //To store result of web api response.   
                var result = responseTask.Result;
                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<DoctorAssignedViewModel>>();
                    readTask.Wait();
                    doctors = readTask.Result;
                    return Json(doctors, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //Error response received   
                    doctors = Enumerable.Empty<DoctorAssignedViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return Json(doctors, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Create(DoctorAssignedViewModel model)
        {
            using (var client = Utility.GetHttpClient())
            {
                string UserID = Session["UserId"].ToString();
                model.AssignedBy = UserID == null ? "0" : UserID;
                model.AssignedDate = Utility.getDefaultTime();
                model.Note.CreatedOn = Utility.getDefaultTime();
                model.Note.CreatedBy = UserID == null ? 0 : Convert.ToInt32(UserID);
                model.Note.RefrenceTableId = 1;
                model.Note.ReferenceId = model.DoctorId.GetValueOrDefault();
                var myContent = JsonConvert.SerializeObject(model);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                var responseTask = client.PostAsync("DoctorAssign/create/", byteContent);
                responseTask.Wait();
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }


            return Json(true, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetBySalePersonId()
        {
            return View();
        }
        public ActionResult GetBySalePersonIdPage(int PageNo = 1 ,string Search = "")
        {
            
            int SalePersonId = Convert.ToInt32(Session["UserId"]);
            
            var data = new  { PageNo, Search, SalePersonId, PageSize = Utility.PageSize  };
            var SerializeObject = JsonConvert.SerializeObject(data);
           
            DoctorAssignedPaginationModel pageModel = new DoctorAssignedPaginationModel();
            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(SerializeObject);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                var responseTask = client.PostAsync("DoctorAssign/GetBySalePersonIdPage/", byteContent);
               
                responseTask.Wait();
                var result = responseTask.Result;
               
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<DoctorAssignedPaginationModel>();
                    readTask.Wait();
                    pageModel = readTask.Result;
                    pageModel.page = new Pager(pageModel.Count, PageNo, Utility.PageSize);
                    pageModel.SearchTerm = Search;
                    ModelState.Clear();
                    return PartialView("_GetBySalePersonIdPartial", pageModel);
                }
                else
                {
                    pageModel.doctorsAssigned = Enumerable.Empty<DoctorAssignedViewModel>();
                    
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return PartialView("_GetBySalePersonIdPartial", pageModel);
        }

        public ActionResult DoctorAssignedList()
        {
            return View();
        }
        public ActionResult DoctorAssignedListPage(int PageNo = 1, string Search = "")
        {
            var data = new { PageNo, Search, PageSize = Utility.PageSize };
            var SerializeObject = JsonConvert.SerializeObject(data);
            DoctorAssignedViewModelPaginationModel pageModel = new DoctorAssignedViewModelPaginationModel();
            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(SerializeObject);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                var responseTask = client.PostAsync("DoctorAssign/getallfordoctor", byteContent);

                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<DoctorAssignedViewModelPaginationModel>();
                    readTask.Wait();
                    pageModel = readTask.Result;
                    pageModel.page = new Pager(pageModel.Count, PageNo, Utility.PageSize);
                    pageModel.SearchTerm = Search;
                    ModelState.Clear();
                    return PartialView("_DoctorAssignedList", pageModel);

                }
                else
                {
                    pageModel.doctorAssignedViewModels = Enumerable.Empty<DoctorAssignedViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return PartialView("_DoctorAssignedList", pageModel);
        }

        public ActionResult ContactedDoctors(int id = 0)
        {
            //TempData["id"] = id;
            ViewBag.UserId = id;
            return View();
        }
        // GET: Contacted Doctors
        public ActionResult ContactedDoctorsPage(int PageNo = 1, string Search = "",int UserId = 0)
        {
           // int UserId = Convert.ToInt32(TempData["id"].ToString());
            var data = new { PageNo, Search, UserId, PageSize = Utility.PageSize };
            var SerializeObject = JsonConvert.SerializeObject(data);
            DoctorPaginationModel pageModel = new DoctorPaginationModel();
           
            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(SerializeObject);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                var responseTask = client.PostAsync("DoctorAssign/ContectedDoctorsPage/", byteContent);
                responseTask.Wait();
                //To store result of web api response.   
                var result = responseTask.Result;
                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<DoctorPaginationModel>();
                    readTask.Wait();
                     pageModel = readTask.Result;
                    pageModel.page = new Pager(pageModel.Count, PageNo, Utility.PageSize);
                    pageModel.SearchTerm = Search;
                    ModelState.Clear();
                    return PartialView("_ContactedDoctorsPartial", pageModel);
                    
                }
                else
                {
                    //Error response received   
                    pageModel.doctors = Enumerable.Empty<DoctorViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return PartialView("_ContactedDoctorsPartial", pageModel);
            //return View(model);
        }

    }
}