using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Vu360Sol.ViewModel.Account;
using Vu360Sol.ViewModel.Doctors;
using Vu360Sol.ViewModel.RequestDemoes;
using Vu360Sol.ViewModel.SalePersons;
using Http = System.Web.HttpContext;

namespace Vu360Sol.Web.Controllers
{
  //  [RoutePrefix("Registration")]
    public class RegistrationController : Controller
    {
        // GET: Registration
        [Route("Account/{id?}")]
        [Route("Registration/Account/{id?}")]
        public ActionResult Account()
        {
            DoctorViewModel doctor = new DoctorViewModel();
            TempData["Doctor"] = null;
            return View(doctor);
        }
        [HttpGet]
        public ActionResult DoctorRegistration(int Id)
        {
            
            DoctorViewModel doctor =  new DoctorViewModel();
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("doctor/getbyid/"+Id);

                responseTask.Wait();
                //To store result of web api response.   
                var result = responseTask.Result;
                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<DoctorViewModel>();
                    readTask.Wait();
                    doctor = readTask.Result;
                    TempData["Doctor"] = doctor;
                }
                else
                {
                    //Error response received   
                    doctor = new DoctorViewModel();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
           
            return View("Account",doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserViewModel model)
        {
            try
            {
                if (model != null)
                {
                    using (var client = Utility.GetHttpClient())
                    {
                        model.RoleId = 2;
                        var myContent = JsonConvert.SerializeObject(model);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        var senddata = await client.PostAsync("user/register", byteContent);
                        var jsonstring = await senddata.Content.ReadAsStringAsync();
                        if (!senddata.IsSuccessStatusCode)
                        {
                            return Json(new { success = false, responseText = jsonstring }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = true, responseText = jsonstring }, JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterDoctor(DoctorViewModel model)
        {
            try
            {
                if (model != null)
                {
                    model.StatusId = 3;
                    model.IsActive = false;
                    model.User.IsActive = false;
                    model.CreatedOn = Utility.getDefaultTime();
                    model.User.CreatedOn = Utility.getDefaultTime();
                    model.CreatedBy = 0;
                    model.User.CreatedBy = 0;
                    var doctor = TempData["Doctor"] as DoctorViewModel;

                    if (doctor != null && doctor.User != null && doctor.User.IsActive)
                    {
                        return Json(new { success = true, responseText = "You are allready registed.Please Login." }, JsonRequestBehavior.AllowGet);
                    }
                    string url = "doctor/create";
                    if (doctor != null && doctor.Id>0)
                    {
                        model.ImagePath = doctor.ImagePath;
                        model.Id = doctor.Id;
                       // model.StatusId = 3;
                        model.IsActive = true;
                        model.User.IsActive = true;
                        model.User.Id = doctor.User.Id;
                        url = "doctor/Update";
                    }
                    
                       
                   
                        
                    using (var client = Utility.GetHttpClient())
                    {
                        
                        var myContent = JsonConvert.SerializeObject(model);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        var senddata = await client.PostAsync(url, byteContent);
                        var jsonstring = await senddata.Content.ReadAsStringAsync();

                        if (!senddata.IsSuccessStatusCode)
                        {
                            return Json(new { success = false, responseText = "Face Error while registered.Please try again later!" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {

                            if (doctor != null && doctor.Id > 0)
                                return Json(new { success = true, responseText = "Registerd Successfully.You can login now." }, JsonRequestBehavior.AllowGet);
                            else
                            {
                                var email = await new Email().DoctorRegistrationEmail(model);
                                if (email)
                                {
                                    return Json(new { success = true, responseText = "Registerd Successfully.You can login once your request is approved." }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(new { success = true, responseText = "Registerd Successfully.Face temporary issue while sending email." }, JsonRequestBehavior.AllowGet);
                                }
                                //return Json(new { success = true, responseText = "Registerd Successfully.You can login once your request is approved." }, JsonRequestBehavior.AllowGet);
                            }
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterSalePerson(SalePersonViewModel model)
        {
            try
            {
                if (model != null)
                {
                    using (var client = Utility.GetHttpClient())
                    {
                        model.User.RoleId = 3;
                        model.User.IsActive = false;
                        model.IsActive = false;
                        model.CreatedOn = Utility.getDefaultTime();
                        model.User.CreatedOn = Utility.getDefaultTime();
                        model.CreatedBy = 0;
                        model.User.CreatedBy = 0;
                        var myContent = JsonConvert.SerializeObject(model);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        var senddata = await client.PostAsync("saleperson/create", byteContent);
                        var jsonstring = await senddata.Content.ReadAsStringAsync();
                        if (!senddata.IsSuccessStatusCode)
                        {
                            return Json(new { success = false, responseText = "Face Error while registered.Please try again later!" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            var email = await new Email().SalePersnRegistrationEmail(model);
                            if (email)
                            {
                                return Json(new { success = true, responseText = "Registerd Successfully.You can login once your request is approved." }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new { success = true, responseText = "Registerd Successfully.Face temporary issue while sending email." }, JsonRequestBehavior.AllowGet);
                            }
                            // return Json(new { success = true, responseText = "Registerd Successfully.You can login once your request is approved." }, JsonRequestBehavior.AllowGet);
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
        public async Task<ActionResult> Login(UserViewModel user)
        {
            if (user != null)
            {
                using (var client = Utility.GetHttpClient())
                {
                    var Client = new HttpClient();
                    var myContent = JsonConvert.SerializeObject(user);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = await client.PostAsync("/user/login", byteContent);
                    if (!response.IsSuccessStatusCode)
                    {
                        return Json(new { success = false, responseText = "Please Enter Valid Username or Password." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var jsonstring = response.Content.ReadAsStringAsync();
                        dynamic details = JObject.Parse(jsonstring.Result);
                        var loginUser = details["userRepo"];
                        var token = details["usertoken"];


                        FormsAuthentication.SetAuthCookie(user.Email, false);
                        Http.Current.Session["UserToken"] = (string)details["usertoken"];
                        Http.Current.Session["Role"] = (string)loginUser["roleId"];
                        Http.Current.Session["UserModel"] = user.Email;
                        Http.Current.Session["UserId"] = (string)loginUser["id"];
                        Http.Current.Session["Email"] = user.Email;
                    

                        return Json(new { success = true, responseText = "Success", roles = Session["Role"] }, JsonRequestBehavior.AllowGet);

                    }
                }

            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = Utility.getDefaultTime().AddYears(-1);//DateTime.Now.AddYears(-1)
            Response.Cookies.Add(cookie1);
            SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            HttpCookie cookie2 = new HttpCookie(sessionStateSection.CookieName, "");
            cookie2.Expires = Utility.getDefaultTime().AddYears(-1);//DateTime.Now.AddYears(-1)
            Response.Cookies.Add(cookie2);

            Session["UserToken"] = null;
            Session["Role"] = null;
            Session["UserModel"] = null;
            Session["UserId"] = null;
            Http.Current.Session["Email"] = null;
            int ClientId = Convert.ToInt32(Session["ClientId"]);
            return RedirectToRoute("Account");
        }
        [HttpPost]
        public async Task<ActionResult> UserNameExsist(UserViewModel user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.Email))
                {
                    using (var client = Utility.GetHttpClient())
                    {
                        // registration.User.RoleId = 2;
                        // var Client = new HttpClient();
                        var myContent = JsonConvert.SerializeObject(user);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        var senddata = await client.PostAsync("user/Usernameexist", byteContent);

                        var jsonstring = await senddata.Content.ReadAsStringAsync();

                        if (!senddata.IsSuccessStatusCode)
                        {
                            Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            return Json(jsonstring, MediaTypeNames.Text.Plain);

                        }
                        else
                        {
                            Response.StatusCode = (int)HttpStatusCode.OK;
                            return Json(jsonstring, MediaTypeNames.Text.Plain);
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

        [Authorize]
        public ActionResult UsersListForApproval()
        {
            IEnumerable<UserViewModel> users = null;
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("user/getallUsers");

                responseTask.Wait();
                //To store result of web api response.   
                var result = responseTask.Result;
                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<UserViewModel>>();
                    readTask.Wait();
                    users = readTask.Result;
                }
                else
                {
                    //Error response received   
                    users = Enumerable.Empty<UserViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(users);
        }

        [Authorize]
        public ActionResult Approve(int Id)
        {
            using (var client = Utility.GetHttpClient())
            {
                var responseTask = client.GetAsync("user/Approve/" + Id + "");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("UsersListForApproval");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return RedirectToAction("UsersListForApproval");
        }

        [Authorize]
        public ActionResult ChangePassword(int id)
        {
            return View();
        }

        [Authorize]
        // For Change Password
        [HttpGet]
        public async Task<ActionResult> ChangePasswordPost(ChangePasswordViewModel model)
        {
            var data = Http.Current.Session["UserId"];
            if (model != null && model.Password !=null && model.NewPassword != null && data != null)
            {
                using (var client = Utility.GetHttpClient())
                {

                    model.Id = Convert.ToInt32(Http.Current.Session["UserId"].ToString());
                    var myContent = JsonConvert.SerializeObject(model);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var responseTask = await client.PostAsync("User/changePassword/", byteContent);
                    var jsonstring = await responseTask.Content.ReadAsStringAsync();
                    //If success received   
                    if (responseTask.IsSuccessStatusCode)
                    {
                        return Json(new { success = true, responseText = jsonstring }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, responseText = jsonstring }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return View();
        }   
        
       
        public ActionResult ForgotPassword()
        {
            return View();
        }

        
        // For Change Password
        [HttpGet]
        public async Task<ActionResult> ForgotPasswordPost(ForgotPasswordViewModel model)
        {
                using (var client = Utility.GetHttpClient())
                {

                    //model.Id = Convert.ToInt32(Http.Current.Session["UserId"].ToString());
                    var myContent = JsonConvert.SerializeObject(model);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var responseTask = await client.PostAsync("User/ForgotPassword/", byteContent);
                    var jsonstring = await responseTask.Content.ReadAsStringAsync();
               
                    //If success received   
                    if (responseTask.IsSuccessStatusCode)
                    {
                        var userData = JsonConvert.DeserializeObject<UserViewModel>(jsonstring);
                        var mail=  await SendEmail(userData);
                        return Json(new { success = true, responseText = jsonstring }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, responseText = jsonstring }, JsonRequestBehavior.AllowGet);
                    }
                }
        } 
        


        private async Task<bool> SendEmail(UserViewModel model)
        {
            Email email = new Email();
            var data = await  email.ForgotPassword(model);
            return true;
        }
    }
}