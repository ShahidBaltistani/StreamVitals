using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vu360Sol.ViewModel;
using Vu360Sol.ViewModel.Visitors;

namespace Vu360Sol.Web.Controllers
{
    public class HomeController : Controller
    {
        
        // GET: Home
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("Practices")]
        public ActionResult Practices()
        {
            return View();
        }

        [Route("Patients")]
        public ActionResult Patients()
        {
            return View();
        }

        [Route("Partners")]
        public ActionResult Partners()
        {
            return View();
        }

        [Route("Insurers")]
        public ActionResult Insurers()
        {
            return View();
        }

        [Route("FAQ")]
        public ActionResult FAQ()
        {
            return View();
        }

        [Route("AboutUs")]
        public ActionResult AboutUs()
        {
            return View();
        }

        [Route("WhyPartner")]
        public ActionResult WhyPartner()
        {
            return View();
        }

        [Route("ContactUs")]
        public ActionResult ContactUs()
        {
            return View();
        }

        [Route("Howworks")]
        public ActionResult Howworks()
        {
            return View();
        }

        [Route("Introduction")]
        public ActionResult Introduction()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SendVisitorEmailIntro(VisitorViewModel visitor)
        {
            try
            {
                if (visitor != null)
                {
                    visitor.CreatedOn = Utility.getDefaultTime();

                    using (var client = Utility.GetHttpClient())
                    {
                        var myContent = JsonConvert.SerializeObject(visitor);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        HttpResponseMessage response = Task.Run(async () => await client.PostAsync("visitor/create/", byteContent)).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var email = await new Email().contactUsEmail(visitor);
                            if (email)
                            {
                                var visitoremail = await new Email().contactVisitorEmail(visitor);
                                if (visitoremail)
                                {
                                    return Json(new { success = true, responseText = "Thank you for your registration. A representative from the StreamVitals team will contact you to discuss next steps." }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new { success = false, responseText = "Face issue while processing request. Please try later." }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                return Json(new { success = false, responseText = "Face issue while processing request. Please try later." }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new { success = false, responseText = "Face issue while processing request.Please try later." }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                return View();
            }
            catch (Exception)
            {
                throw;
            }


        }


        [Route("Introenvelope")]
        public ActionResult Introenvelope()
        {
            return View();
        }

        [Route("Introductionenvelope")]
        public ActionResult Introductionenvelope()
        {
            return View();
        }

        [Route("Introletter")]
        public ActionResult Introletter()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> SendVisitorEmail(VisitorViewModel visitor)
        {
            try
            {
                if (visitor != null)
                {
                    using (var client = Utility.GetHttpClient())
                    {
                        // visitor.CreatedOn = DateTime.Now;
                        visitor.CreatedOn = Utility.getDefaultTime();
                        var myContent = JsonConvert.SerializeObject(visitor);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        var senddata = await client.PostAsync("user/addVisitor", byteContent);
                        var jsonstring = await senddata.Content.ReadAsStringAsync();
                        if (senddata.IsSuccessStatusCode)
                        {
                            var email = await new Email().contactUsEmail(visitor);
                            if (email)
                            {
                                return Json(new { success = true, responseText = "Email Sent Successfully. Thank you for contacting us." }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = false, responseText = "Face issue while sending email. Please try later." }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        //else
                        //{
                        //    return Json(new { success = false, responseText = "Face issue while sending email. Please try later." }, JsonRequestBehavior.AllowGet);
                        //}
                    }
                }
                return View();
            }
            catch (Exception)
            {
                throw;
            }
            
           
        }
    }
}