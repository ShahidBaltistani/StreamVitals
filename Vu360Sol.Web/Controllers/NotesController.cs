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
using Vu360Sol.ViewModel.Notes;
using Vu360Sol.ViewModel.RequestDemoes;

namespace Vu360Sol.Web.Controllers
{
    public class NotesController : Controller
    {
        [HttpPost]
        public JsonResult Insert(NoteViewModel model)
        {
            using (var client = Utility.GetHttpClient())
            {
                var myContent = JsonConvert.SerializeObject(model);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
                var responseTask = client.PostAsync("note/create/", byteContent);
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
            return Json(false);
        }
        public JsonResult List(int Id)
        {
            int RequestDemoId = Id;
            List<NoteViewModel> notes = new List<NoteViewModel>();
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("note/GetAllNotesByRequestDemoId/" + RequestDemoId + "");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<NoteViewModel>>();
                    readTask.Wait();
                    notes = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return Json(notes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.DeleteAsync("note/delete/" + id + "");
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