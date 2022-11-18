using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Vu360Sol.ViewModel.SharedViewModels;
using Vu360Sol.ViewModel.Visitors;

namespace Vu360Sol.Web.Controllers
{
    [Authorize]
    public class InquiryController : Controller
    {
        // GET: Inquiry
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllVisitorForLearning(int PageNo = 1, string Search = "")
        {
            var data = new { PageNo, Search, PageSize = Utility.PageSize };
            var SerializeObject = JsonConvert.SerializeObject(data);
            VisitorViewModelPaginationModel pageModel = new VisitorViewModelPaginationModel();

            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(SerializeObject);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                var responseTask = client.PostAsync("visitor/GetAllVisitorForLearning/", byteContent);
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
        
        public PartialViewResult GetAllVisitorForStarting(int PageNo = 1, string Search = "")
        {
            var data = new { PageNo, Search, PageSize = Utility.PageSize };
            var SerializeObject = JsonConvert.SerializeObject(data);
            VisitorViewModelPaginationModel pageModel = new VisitorViewModelPaginationModel();

            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(SerializeObject);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                var responseTask = client.PostAsync("visitor/GetAllVisitorForStarting/", byteContent);
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