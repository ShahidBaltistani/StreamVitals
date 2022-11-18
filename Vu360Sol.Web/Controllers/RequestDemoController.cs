using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vu360Sol.ViewModel.RequestDemoes;
using Vu360Sol.ViewModel.SharedViewModels;

namespace Vu360Sol.Web.Controllers
{
    public class RequestDemoController : Controller
    {
        public ActionResult Requests()
        {
            return View();
        }
        [HttpGet]
        public ActionResult RequestsPage(int PageNo = 1, string Search = "")
        {
            var data = new { PageNo, Search, PageSize = Utility.PageSize };
            var SerializeObject = JsonConvert.SerializeObject(data);
            RequestDemoViewModelPaginationModel pageModel = new RequestDemoViewModelPaginationModel();
            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(SerializeObject);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                var responseTask = client.PostAsync("requestdemo/getall", byteContent);

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

                    return PartialView("_RequestDemoPartial", pageModel);
                }
                else
                {
                    pageModel.requestDemoViewModels = Enumerable.Empty<RequestDemoViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return PartialView("_RequestDemoPartial", pageModel);
        }
        // GET: RequestDemo
        [HttpGet]
        public ActionResult Schedule()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Schedule(RequestDemoViewModel model,string Date)
        {
            int UserID = Convert.ToInt32(Session["UserId"]);
            model.Date = Convert.ToDateTime(DateTime.ParseExact(Date, "MM/dd/yyyy", CultureInfo.InvariantCulture));
            using (var client = Utility.GetHttpClient())
            {
                model.CreatedBy = UserID == 0 ? 0 : UserID;
                model.CreatedOn = Utility.getDefaultTime();
                var myContent = JsonConvert.SerializeObject(model);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                var responseTask = client.PostAsync("requestdemo/create/", byteContent);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var email = await new Email().RequestDemoEmail(model.Email);
                    if (email)
                    {
                        return RedirectToAction("Thanks");
                    }
                    else
                    {
                        return RedirectToAction("EmailError");
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
        public ActionResult Thanks()
        {
            return View();
        }
        public ActionResult EmailError()
        {
            return View();
        }

        public ActionResult GetById(int id)
        {
            RequestDemoViewModel requestDemo = null;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("requestdemo/GetById/" + id + "");
                responseTask.Wait();
                //To store result of web api response.   
                var result = responseTask.Result;
                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<RequestDemoViewModel>();
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