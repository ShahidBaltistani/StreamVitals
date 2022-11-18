using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vu360Sol.ViewModel.Common;
using Vu360Sol.ViewModel.Doctors;
using Vu360Sol.ViewModel.SalePersons;
using Vu360Sol.ViewModel.SharedViewModels;

namespace Vu360Sol.Web.Controllers
{
    [Authorize]
    public class ApprovalController : Controller
    {
        // GET: Approval
        public ActionResult ApprovalsList()
        {
            return View();
        }
        public ActionResult Doctors(int PageNo = 1, string Search = "")
        {
            var data = new { PageNo, Search, PageSize = Utility.PageSize };
            var SerializeObject = JsonConvert.SerializeObject(data);
            DoctorViewModelPaginationModel pageModel = new DoctorViewModelPaginationModel();

            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(SerializeObject);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");

                var responseTask = client.PostAsync("doctor/getallinactive/", byteContent);
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
        public ActionResult SalePersons(int PageNo = 1, string Search = "")
        {
            var data = new { PageNo, Search, PageSize = Utility.PageSize };
            var SerializeObject = JsonConvert.SerializeObject(data);
            SalePersonViewModelPaginationModel pageModel = new SalePersonViewModelPaginationModel();
            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(SerializeObject);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                var responseTask = client.PostAsync("saleperson/getallinactive/", byteContent);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<SalePersonViewModelPaginationModel>();
                    readTask.Wait();
                    pageModel = readTask.Result;
                    pageModel.page = new Pager(pageModel.Count, PageNo, Utility.PageSize);
                    pageModel.SearchTerm = Search;
                    ModelState.Clear();
                    return PartialView("_SalePersonListPartial", pageModel);
                }
                else
                {
                    pageModel.salePersonViewModels = Enumerable.Empty<SalePersonViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return PartialView("_SalePersonListPartial", pageModel);
        }

        public ActionResult SalePersonApproval(int Id)
        {
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("saleperson/SalePersonApproval/" + Id + "");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("ApprovalsList");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return RedirectToAction("ApprovalsList");
        }
        public ActionResult DoctorApproval(int Id)
        {
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("doctor/DoctorApproval/" + Id + "");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("ApprovalsList");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return RedirectToAction("ApprovalsList");
        }
    }
}