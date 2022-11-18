using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vu360Sol.ViewModel;
using Vu360Sol.ViewModel.RequestDemoes;
using Vu360Sol.ViewModel.Account;
using System.Net.Http;
using Vu360Sol.ViewModel.Visitors;
using Vu360Sol.ViewModel.SharedViewModels;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Vu360Sol.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Home()
        {
            return View();
        }


        /// <summary>
        public ActionResult RequestDemoListPage(int PageNo = 1, string Search = "", int Days = 1)
        {
            ViewBag.Register = false;
            var data = new { PageNo, Search, PageSize = Utility.PageSize, Days };
            var SerializeObject = JsonConvert.SerializeObject(data);
            RequestDemoViewModelPaginationModel pageModel = new RequestDemoViewModelPaginationModel();

            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(SerializeObject);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                var responseTask = client.PostAsync("Dashboard/GetRequestDemo", byteContent);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<RequestDemoViewModelPaginationModel>();
                    readTask.Wait();
                    pageModel = readTask.Result;
                    pageModel.page = new Pager(pageModel.Count, PageNo, Utility.PageSize);
                    pageModel.SearchTerm = Search;
                    ModelState.Clear();
                    return PartialView("_RequestDemo", pageModel);
                }
                else
                {
                    //Error response received   
                    pageModel.requestDemoViewModels = Enumerable.Empty<RequestDemoViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return PartialView("_RequestDemo", pageModel);
        }
        public ActionResult Approval(int PageNo = 1,  string Search = "", int Days = 1)
        {
            ViewBag.Register = false;
            var data = new { PageNo, Search, PageSize = Utility.PageSize,
                Days
            };
            var SerializeObject = JsonConvert.SerializeObject(data);
            UserViewModelPaginationModel pageModel = new UserViewModelPaginationModel();

            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(SerializeObject);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                var responseTask = client.PostAsync("Dashboard/getallinactiveUsers", byteContent);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<UserViewModelPaginationModel>();
                    readTask.Wait();
                    pageModel = readTask.Result;
                    pageModel.page = new Pager(pageModel.Count, PageNo, Utility.PageSize);
                    pageModel.SearchTerm = Search;
                    ModelState.Clear();
                    return PartialView("_ApprovalPartial", pageModel);
                }
                else
                {
                    //Error response received   
                    pageModel.userViewModels = Enumerable.Empty<UserViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return PartialView("_ApprovalPartial", pageModel);
        }
        public ActionResult UserApproval(int Id)
        {
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("Dashboard/Approve/" + Id + "");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return RedirectToAction("Home");
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
    }
}