using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vu360Sol.ViewModel.Practices;
using Vu360Sol.ViewModel.SharedViewModels;

namespace Vu360Sol.Web.Controllers
{
    public class PracticeController : Controller
    {
        // GET: Practice
        public ActionResult PracticeList()
        {
            return View();
        }
        public ActionResult GetAll(int PageNo = 1, string Search = "")
        {
            ViewBag.Register = false;
            var data = new { PageNo, Search, PageSize = Utility.PageSize };
            var SerializeObject = JsonConvert.SerializeObject(data);
            PracticeViewModelPaginationModel pageModel = new PracticeViewModelPaginationModel();

            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(SerializeObject);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                var responseTask = client.PostAsync("practice/getall", byteContent);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<PracticeViewModelPaginationModel>();
                    readTask.Wait();
                    pageModel = readTask.Result;
                    pageModel.page = new Pager(pageModel.Count, PageNo, Utility.PageSize);
                    pageModel.SearchTerm = Search;
                    ModelState.Clear();
                    return PartialView("_PracticePartial", pageModel);
                }
                else
                {
                    //Error response received   
                    pageModel.practice = Enumerable.Empty<PracticeViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return PartialView("_PracticePartial", pageModel);
        }

        public JsonResult GetAllPractice()
        {
            IEnumerable<PracticeViewModel> model = null;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("Practice/getallPractice");

                responseTask.Wait();
                //To store result of web api response.   
                var result = responseTask.Result;
                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<PracticeViewModel>>();
                    readTask.Wait();
                    model = readTask.Result;
                }
                else
                {
                    //Error response received   
                    model = Enumerable.Empty<PracticeViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }


        // For Change Password
        [HttpPost]
        public async Task<ActionResult> Create(PracticeViewModel model)
        {
            using (var client = Utility.GetHttpClient())
            {

                var myContent = JsonConvert.SerializeObject(model);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var responseTask =client.PostAsync("Practice/Create/", byteContent);
                responseTask.Wait();
                var result = responseTask.Result;
                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("PracticeList");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }

            }
            return View("PracticeList");
        }



        [HttpGet]
        public ActionResult Edit(int id)
        {
            PracticeViewModel model;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("practice/getbyid/" + id + "");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<PracticeViewModel>();
                    model = readTask.Result;
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    return View();
                }

            }
        }
        [HttpPost]
        public ActionResult Edit(PracticeViewModel model)
        {
            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(model);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var responseTask =client.PostAsync("Practice/edit/", byteContent);
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("PracticeList");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }

            }
            return View("PracticeList");
        }
        public JsonResult Delete(int id)
        {
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("Practice/delete/" + id + "");
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
    }
}