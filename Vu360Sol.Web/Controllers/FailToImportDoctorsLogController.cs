using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Vu360Sol.ViewModel.Logs;
using Vu360Sol.ViewModel.SharedViewModels;

namespace Vu360Sol.Web.Controllers
{
    public class FailToImportDoctorsLogController : Controller
    {
        // GET: FailToImportDoctorsLog
        public ActionResult FailToImportDoctorsLog()
        {
            return View();
        }
        public ActionResult FailToImportDoctorsPage(int PageNo = 1, string Search = "")
        {
            var data = new { PageNo, Search, PageSize = Utility.PageSize };
            var SerializeObject = JsonConvert.SerializeObject(data);
            FailToImportDoctorsLogPaginationModel pageModel = new FailToImportDoctorsLogPaginationModel();
            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(SerializeObject);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                var responseTask = client.PostAsync("FailToImportDoctorsLog/GetAllPage/", byteContent);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<FailToImportDoctorsLogPaginationModel>();
                    readTask.Wait();
                    pageModel = readTask.Result;
                    pageModel.page = new Pager(pageModel.Count, PageNo, Utility.PageSize);
                    pageModel.SearchTerm = Search;
                    ModelState.Clear();
                    return PartialView("_FailToImportDoctorsLogPartial", pageModel);

                }
                else
                {
                    pageModel.FailToImportDoctorsLogViewModels = Enumerable.Empty<FailToImportDoctorsLogViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return PartialView("_FailToImportDoctorsLogPartial", pageModel);
        }
    }
}