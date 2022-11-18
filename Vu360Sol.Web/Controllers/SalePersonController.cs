using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vu360Sol.ViewModel.Common;
using Vu360Sol.ViewModel.SalePersons;
using Vu360Sol.ViewModel.SharedViewModels;

namespace Vu360Sol.Web.Controllers
{
    [Authorize]
    public class SalePersonController : Controller
    {
        // GET: SalePerson
        public ActionResult SalePersonList()
        {
            return View();
        }
        public ActionResult SalePersonListPage(int PageNo = 1, string Search = "")
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
                var responseTask = client.PostAsync("saleperson/getall", byteContent);
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
        [HttpGet]
        public ActionResult CreateSalePerson()
        {
            List<GenderViewModel> genders = null;
            using (var client = Utility.GetHttpClient())
            {
                var model = Task.Run(async () => await client.GetAsync("saleperson/getallgender")).Result;
                if (model.IsSuccessStatusCode)
                {
                    genders = Task.Run(async () => await model.Content.ReadAsAsync<List<GenderViewModel>>()).Result;
                }
                ViewBag.Gender = genders;
            }
                return View();
        }
        [HttpPost]
        public ActionResult CreateSalePerson(SalePersonViewModel model,HttpPostedFileBase file)
        {

            string FinalResult = null;
            if (file != null)
            {
                string filename = file.FileName.ToLower().ToString();
                string[] exts = filename.Split('.');
                string name = exts[0].ToString() + Utility.getDefaultTime().ToString("yyyyMMddHHmmss");//DateTime.Now.ToString("yyyyMMddHHmmss")
                string ext = exts[1].ToString();
                FinalResult = name + "." + ext;
                string path = Path.Combine(Server.MapPath("~/UploadedFiles/"), Path.GetFileName(FinalResult));
                file.SaveAs(path);
            }
            int UserID = Convert.ToInt32(Session["UserId"]);

            model.ImagePath = FinalResult;
            model.User.RoleId = 3;
            model.User.CreatedOn = Utility.getDefaultTime();
            model.User.CreatedBy = UserID;
            model.CreatedOn = Utility.getDefaultTime();
            model.CreatedBy = UserID;
            model.ImagePath = FinalResult;
            model.User.IsActive = true;
            model.IsActive = true;
            using (var client = Utility.GetHttpClient()) 
            {
                var myContent = JsonConvert.SerializeObject(model);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = Task.Run(async () => await client.PostAsync("saleperson/create/", byteContent)).Result;
            }
            return RedirectToAction("SalePersonList");
        }
        [HttpGet]
        public ActionResult EditSalePerson(int id)
        {
            List<GenderViewModel> genders = null;
            SalePersonViewModel model;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("saleperson/getbyid/" + id + "");
                responseTask.Wait();
                var result = responseTask.Result;
                var readTask = result.Content.ReadAsAsync<SalePersonViewModel>();
                readTask.Wait();
                model = readTask.Result;


                var gendermodel = Task.Run(async () => await client.GetAsync("saleperson/getallgender")).Result;
                if (gendermodel.IsSuccessStatusCode)
                {
                    genders = Task.Run(async () => await gendermodel.Content.ReadAsAsync<List<GenderViewModel>>()).Result;
                }
                ViewBag.Gender = genders;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult EditSalePerson(SalePersonViewModel model, HttpPostedFileBase file)
        {
            if (file==null)
            {
                model.ImagePath = model.ImagePath;
            }
            else
            {
                string filename = file.FileName.ToLower().ToString();
                string[] exts = filename.Split('.');
                string name = exts[0].ToString() + Utility.getDefaultTime().ToString("yyyyMMddHHmmss");// DateTime.Now.ToString("yyyyMMddHHmmss")
                string ext = exts[1].ToString();
                string FinalResult = name + "." + ext;
                string _ImagesPath = "~/UploadedFiles/";

                // Save New File....
                string path = Path.Combine(Server.MapPath(_ImagesPath), Path.GetFileName(FinalResult));
                file.SaveAs(path);

                model.ImagePath = FinalResult;
            }
            using (var client = Utility.GetHttpClient())
            {
                int UserID = Convert.ToInt32(Session["UserId"]);
                var salePerson = new SalePersonViewModel();
                var responseT = client.GetAsync("salePerson/getbyid/" + model.Id + "");
                responseT.Wait();
                var dr = responseT.Result.Content.ReadAsAsync<SalePersonViewModel>();
                salePerson = dr.Result;
                if (salePerson != null)
                {
                    model.CreatedOn = salePerson.CreatedOn == null ? DateTime.Now : salePerson.CreatedOn;
                    model.User.CreatedOn = salePerson.User.CreatedOn == null ? DateTime.Now : salePerson.User.CreatedOn;
                    model.CreatedBy = salePerson.CreatedBy == null ? 1 : salePerson.User.CreatedBy;
                    model.User.CreatedBy = salePerson.User.CreatedBy == null ? 1 : salePerson.User.CreatedBy;
                }
                model.User.ModifiedOn = Utility.getDefaultTime();
                model.User.ModifiedBy = UserID == 0 ? 0 : UserID;
                model.ModifiedOn = Utility.getDefaultTime();
                model.ModifiedBy = UserID == 0 ? 0 : UserID;
                var myContent = JsonConvert.SerializeObject(model);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = Task.Run(async () => await client.PostAsync("saleperson/update/", byteContent)).Result;
            }
            return RedirectToAction("SalePersonList");
        }
        public JsonResult Delete(int id)
        {
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("saleperson/delete/" + id + "");
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
        public JsonResult SalePersonDropDown()
        {
            IEnumerable<SalePersonViewModel> salePeople = null;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("saleperson/getall");

                responseTask.Wait();
                //To store result of web api response.   
                var result = responseTask.Result;
                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<SalePersonViewModel>>();
                    readTask.Wait();
                    salePeople = readTask.Result;
                }
                else
                {
                    //Error response received   
                    salePeople = Enumerable.Empty<SalePersonViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return Json(salePeople, JsonRequestBehavior.AllowGet);
        }     
        
        
 
    }
}