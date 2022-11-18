using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Vu360Sol.ViewModel.Doctors;
using Vu360Sol.ViewModel.SalePersons;
using Vu360Sol.ViewModel.SharedViewModels;
using Vu360Sol.ViewModel.Visitors;

namespace Vu360Sol.Web.Controllers
{
    public class SalePersonDashboardController : Controller
    {
        // GET: SalePersonDashboard
        public ActionResult Index()
        {
            return View();
        }
         public ActionResult ContactedDoctorsPage(int PageNo = 1, string Search = "",int Days = 1)
          {
            // var t = Session["UserId"];
           var SalePersonId = Convert.ToInt32(Session["UserId"]);
            var data = new { PageNo, Search, Days, SalePersonId, PageSize = Utility.PageSize };
            var SerializeObject = JsonConvert.SerializeObject(data);
            DoctorPaginationModel pageModel = new DoctorPaginationModel();
           
            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(SerializeObject);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                var responseTask = client.PostAsync("SalePersonDashboard/ContactedDoctors/", byteContent);
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
                    return PartialView("_ContactedDoctorsDB", pageModel);
                    
                }
                else
                {
                    //Error response received   
                    pageModel.doctors = Enumerable.Empty<DoctorViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return PartialView("_ContactedDoctorsDB", pageModel);
            //return View(model);
        }

        public ActionResult DoctorAssignedList(int PageNo = 1, string Search = "", int Days = 1)
        {

           var SalePersonId = Convert.ToInt32(Session["UserId"]);

            var data = new { PageNo, Search, Days, SalePersonId, PageSize = Utility.PageSize };
            var SerializeObject = JsonConvert.SerializeObject(data);

            DoctorAssignedPaginationModel pageModel = new DoctorAssignedPaginationModel();
            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(SerializeObject);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                var responseTask = client.PostAsync("SalePersonDashboard/DoctorAssignedList/", byteContent);

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
                    return PartialView("_DoctorAssignedListDB", pageModel);
                }
                else
                {
                    pageModel.doctorsAssigned = Enumerable.Empty<DoctorAssignedViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return PartialView("_DoctorAssignedListDB", pageModel);
        }

        public ActionResult GetAllVisitorForLearning(int PageNo = 1, string Search = "", int Days = 1)
        {
            var data = new { PageNo, Search, PageSize = Utility.PageSize, Days };
            var SerializeObject = JsonConvert.SerializeObject(data);
            VisitorViewModelPaginationModel pageModel = new VisitorViewModelPaginationModel();

            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(SerializeObject);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                var responseTask = client.PostAsync("Dashboard/GetAllVisitorForLearning/", byteContent);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<VisitorViewModelPaginationModel>();
                    readTask.Wait();
                    pageModel = readTask.Result;
                    pageModel.page = new Pager(pageModel.Count, PageNo, Utility.PageSize);
                    pageModel.SearchTerm = Search;
                    ModelState.Clear();
                    return PartialView("_VisitorsLearningPartial", pageModel);

                }
                else
                {
                    pageModel.visitorViewModels = Enumerable.Empty<VisitorViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return PartialView("_VisitorsLearningPartial", pageModel);
        }

        public PartialViewResult GetAllVisitorForStarting(int PageNo = 1, string Search = "", int Days = 1)
        {
            var data = new { PageNo, Search, PageSize = Utility.PageSize, Days };
            var SerializeObject = JsonConvert.SerializeObject(data);
            VisitorViewModelPaginationModel pageModel = new VisitorViewModelPaginationModel();

            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(SerializeObject);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                var responseTask = client.PostAsync("Dashboard/GetAllVisitorForStarting/", byteContent);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<VisitorViewModelPaginationModel>();
                    readTask.Wait();
                    pageModel = readTask.Result;
                    pageModel.page = new Pager(pageModel.Count, PageNo, Utility.PageSize);
                    pageModel.SearchTerm = Search;
                    ModelState.Clear();
                    return PartialView("_VisitorsStartingPartial", pageModel);

                }
                else
                {
                    pageModel.visitorViewModels = Enumerable.Empty<VisitorViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return PartialView("_VisitorsStartingPartial", pageModel);
        }
        public ActionResult GetById(int id)
        {
            DoctorAssignedViewModel requestDemo = null;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("SalePersonDashboard/GetById/" + id + "");
                responseTask.Wait();
                //To store result of web api response.   
                var result = responseTask.Result;
                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<DoctorAssignedViewModel>();
                    readTask.Wait();
                    requestDemo = readTask.Result;
                    return Json(requestDemo, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //Error response received   
                    requestDemo = null;
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return Json(requestDemo, JsonRequestBehavior.AllowGet);
        }


    }
}