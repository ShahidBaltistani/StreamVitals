using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Vu360Sol.ViewModel.RequestDemoes;
using Http = System.Web.HttpContext;

namespace Vu360Sol.Web.Controllers
{
    [Authorize]
    public class DoctorScheduleController : Controller
    {
        // GET: DoctorSchedule
        public ActionResult DocSchedule()
        {
            return View();
        }
        public ActionResult DocScheduleList()
        {
            IEnumerable<RequestDemoViewModel> Appointments = null;
            using (var client = Utility.GetHttpClient())
            {
                int UserId = Convert.ToInt32(Http.Current.Session["UserId"].ToString());


                var responseTask = client.GetAsync("requestdemo/getallbydoctorid/" + UserId + "");
                responseTask.Wait();
                //To store result of web api response.   
                var result = responseTask.Result;
                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<RequestDemoViewModel>>();
                    readTask.Wait();
                    Appointments = readTask.Result;
                }
                else
                {
                    //Error response received   
                    Appointments = Enumerable.Empty<RequestDemoViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return Json(Appointments);
        }
    }
}