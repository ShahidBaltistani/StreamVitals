using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Vu360Sol.ViewModel;
using Vu360Sol.ViewModel.Account;
using Vu360Sol.ViewModel.Doctors;
using Vu360Sol.ViewModel.RequestDemoes;
using Vu360Sol.ViewModel.SalePersons;
using Vu360Sol.ViewModel.Visitors;
using static System.Net.WebRequestMethods;

namespace Vu360Sol.Web
{
    public class Email
    {
        //public string FromMail { get; set; } = "info@streamvitals.com";
        //public string FromPassword { get; set; } = "vu@360.info@StreamVitals";
       public string FromSubject { get; set; } = "Thanks";
        //public string Host { get; set; } = "smtp.gmail.com";
        //public string Port { get; set; } = 587;
        public static string FromMail { get; set; } 
        public static string FromPassword { get; set; }
        public static string Host { get; set; }
        public static int Port { get; set; }
        public Email()
        {
            if(string.IsNullOrEmpty(FromMail) || string.IsNullOrEmpty(FromPassword) || string.IsNullOrEmpty(Host))
            {
                var configrations = EmailConfigurationSetup.GetConfigration();
                if(configrations != null)
                {
                    FromMail = configrations.Email;
                    FromPassword = configrations.Password;
                    Host = configrations.Host;
                    Port = configrations.Port;
                }
            }
        }


        #region Mail Through MailKit
        //public async Task<bool> contactUsEmail(VisitorViewModel visitor)
        //{
        //    try
        //    {
        //        var message = new MimeMessage();
        //        message.From.Add(new MailboxAddress("Stream Vitals", "info@streamvitals.com"));
        //        message.To.Add(new MailboxAddress("" + visitor.FullName + "", "" + visitor.Email + ""));
        //        message.Subject = "Thanks";

        //        string str = @"
        //        <center>
        //        <div style = 'font-size:20px;font-family:' Century Gothic';'>
        //        <div style = 'background-color:#DCDCDC;color:#060606;width:100%;padding:20px;' >
        //        <table style='width:100%;text-align: left;'>
        //        <tr>
        //            <th>Name</th>
        //            <th>Contact</th>
        //            <th>Email</th>
        //            <th>Message</th>
        //        </tr>
        //        <tr>
        //            <td>'" + visitor.FullName + @"'</td>
        //            <td>'" + visitor.Phone + @"'</td>
        //            <td>'" + visitor.Email + @"'</td>
        //            <td>'" + visitor.Message + @"'</td>
        //        </tr>
        //        </table>
        //        </div>
        //        </div>
        //        <span style = 'font-size:20px;font-family:' Century Gothic';font-weight:bold'>.....Thank You For choosing us.....</span>
        //        </center>";
        //        var bodyBuilder = new BodyBuilder();
        //        bodyBuilder.HtmlBody = str;
        //        message.Body = bodyBuilder.ToMessageBody();
        //        using (var emailClient = new SmtpClient())
        //        {
        //            emailClient.Connect("smtp.gmail.com", 587);
        //            emailClient.Authenticate("info@streamvitals.com", "vu@360.info@StreamVitals");
        //            await emailClient.SendAsync(message);
        //            emailClient.Disconnect(true);
        //        }
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        #endregion

        public async Task<bool> contactUsEmail(VisitorViewModel visitor)
        {
            try
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(visitor.Email));
                message.From = new MailAddress(FromMail); 
               
                message.IsBodyHtml = true;
                if (visitor.VisitorPurposeId == 3)
                {
                    message.Subject = "Thanks for your interest in Streamvitals";
                }
                else if (visitor.VisitorPurposeId == 4)
                {
                    message.Subject = "Thanks for your interest in Streamvitals.Get ready to start.";
                }
                else
                {
                    message.Subject = "Thanks";
                 
                }
                message.AlternateViews.Add(visitor_Body(visitor));

                using (var smtp = new System.Net.Mail.SmtpClient())
                {
                    smtp.UseDefaultCredentials = true;
                    var credential = new NetworkCredential
                    {
                        UserName = FromMail,
                        Password = FromPassword
                    };
                    smtp.Credentials = credential;
                    smtp.Host = Host;
                    smtp.Port = Port;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        
        public async Task<bool> contactVisitorEmail(VisitorViewModel visitor)
        {
            try
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(FromMail));//
                message.From = new MailAddress(FromMail); message.Subject = FromSubject;
                message.IsBodyHtml = true;
                message.AlternateViews.Add(visitor_Body_StreamVitals(visitor));

                using (var smtp = new System.Net.Mail.SmtpClient())
                {
                    smtp.UseDefaultCredentials = true;
                    var credential = new NetworkCredential
                    {
                        UserName = FromMail,
                        Password = FromPassword
                    };
                    smtp.Credentials = credential;
                    smtp.Host = Host;
                    smtp.Port = Port;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DoctorRegistrationEmail(DoctorViewModel doctor)
        {
            try
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(doctor.User.Email));
                message.From = new MailAddress(FromMail); message.Subject = FromSubject;
                message.IsBodyHtml = true;
                message.AlternateViews.Add(doctorRegistration_Body(doctor));

                using (var smtp = new System.Net.Mail.SmtpClient())
                {
                    smtp.UseDefaultCredentials = true;
                    var credential = new NetworkCredential
                    {
                        UserName = FromMail,
                        Password = FromPassword
                    };
                    smtp.Credentials = credential;
                    smtp.Host = Host;
                    smtp.Port = Port;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> SalePersnRegistrationEmail(SalePersonViewModel SalePerson)
        {
            try
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(SalePerson.User.Email));
                message.From = new MailAddress(FromMail); message.Subject = FromSubject;
                message.IsBodyHtml = true;
                message.AlternateViews.Add(salePersonRegistration_Body(SalePerson));

                using (var smtp = new System.Net.Mail.SmtpClient())
                {
                    smtp.UseDefaultCredentials = true;
                    var credential = new NetworkCredential
                    {
                        UserName = FromMail,
                        Password = FromPassword
                    };
                    smtp.Credentials = credential;
                    smtp.Host = Host;
                    smtp.Port = Port;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        private AlternateView visitor_Body(VisitorViewModel visitor)
        {
            string str = "";
            if (visitor.VisitorPurposeId == 3)
            {
                str = @"
                <section id='content'>
                    <div class='container center clearfix>
                    <div class='heading-block center'>
                        <div>
                            Thank you for your interest in StreamVitals remote patient monitoring platform.  A member of StreamVitals will be in contact with you to share how this can help your business.  In the meantime, please review our website at www.streamvitals.com.  
                        </div>
                     <div><br/>Best Regards,<br/>
                        StreamVitals Team
                     </div>
                    </div>
                    </div>
                    </section>";
            }
           else if (visitor.VisitorPurposeId == 4)
            {
                str = @"
                <section id='content'>
                    <div class='container center clearfix>
                    <div class='heading-block center'>
                        <div>
                    
                        StreamVitals is excited that you are ready to sign up for our Remote Patient Monitoring Platform. A StreamVitals team member will contact you for next steps.  
                        </div>
                     <div><br/>Best Regards,<br/>
                        StreamVitals Team
                     </div>
                    </div>
                    </div>
                    </section>";
            }
            else
            {
                str = @"
        <section id='content'>
            <div class='container center clearfix>
            <div class='heading-block center'>
                <div>Thank you for your interest in StreamVitals, your one stop shop for automated Remote Patient Monitoring.  For more information, you can  <a href='https://streamvitals.com' target='_blank'>click here</a>.  A StreamVitals care coordinator will be in contact with you to discuss how our platform works.  
                </div>
             <div><br/>Best Regards,<br/>
                StreamVitals Team
             </div>
            </div>
            </div>
            </section>";

            }
             
            AlternateView AV =
            AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);

            return AV;
        }

        private AlternateView doctorRegistration_Body(DoctorViewModel doctor)
        {
            string str = @"
        <section id='content'>
            <div class='container center clearfix>
            <div class='heading-block center'>
                <div>Thank you for your interest in StreamVitals, your one stop shop for automated Remote Patient Monitoring.  For more information, you can  <a href='https://streamvitals.com' target='_blank'>click here</a>.  A StreamVitals care coordinator will be in contact with you to discuss how our platform works.  
                </div>
             <div><br/>Best Regards,<br/>
                StreamVitals Team
             </div>
            </div>
            </div>
            </section>";
            AlternateView AV =
            AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);

            return AV;
        }
        private AlternateView salePersonRegistration_Body(SalePersonViewModel doctor)
        {
            string str = @"
        <section id='content'>
            <div class='container center clearfix>
            <div class='heading-block center'>
                <div>Thank you for your interest in StreamVitals, your one stop shop for automated Remote Patient Monitoring.  For more information, you can  <a href='https://streamvitals.com' target='_blank'>click here</a>.  A StreamVitals care coordinator will be in contact with you to discuss how our platform works.  
                </div>
             <div><br/>Best Regards,<br/>
                StreamVitals Team
             </div>
            </div>
            </div>
            </section>";
            AlternateView AV =
            AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);

            return AV;
        }
     

        private AlternateView visitor_Body_StreamVitals(VisitorViewModel visitor)
        {

            string strparam;
            string purpose;
            if(visitor.VisitorPurposeId == 3)
            {
                purpose = "Learn More";
            }
            else
            {
                purpose = "Get Started";

            }
            strparam = @" <center>
            <div style='font-size:20px;font-family:' Century Gothic';'>
            <h1>Visitor Details</h1>
            <div style='background-color:#DCDCDC;color:#060606;width:100%;padding:20px;'>
            <table style='width:100%;text-align: left;'>
                <tr>
                    <th>Date</th>
                    <th>Name</th>
                    <th>Email</th>
                    <th>Phone</th>
                    <th>Practice</th>
                    <th>Purpose</th>
                </tr>
                <tr>
                    
                    <td>" + Utility.getDefaultTime() + @"</td>
                    <td>" + visitor.FirstName + @"</td>
                    <td>" + visitor.Email + @"</td>
                    <td>" + visitor.Phone + @"</td>
                    <td>" + visitor.Practice + @"</td>
                    <td>" + purpose + @"</td>
                </tr>
            </table>
            </div>
            </div>
            //<span style='font-size:20px;font-family:' Century Gothic';font-weight:bold'>.....Thank You For choosing us.....</span>
            //</center>";
            //strparam = " < br/>Visitor Details: ";
            //strparam = strparam + "<br/>Name: " + visitor.FirstName;
            //strparam = strparam + "<br/>Email: " + visitor.Email;
            //strparam = strparam + "<br/>Phone: " + visitor.Phone;
            //strparam = strparam + "<br/>Practice: " + visitor.Practice;
            //strparam = strparam + "<br/>Purpose: " + purpose;
            //strparam += "<br/><br/> ";

            string str = strparam + @"
        <section id='content'>
            <div class='container center clearfix>
            <div class='heading-block center'>
                <div>Thank you for your interest in StreamVitals, your one stop shop for automated Remote Patient Monitoring.  For more information, you can  <a href='https://streamvitals.com' target='_blank'>click here</a>.  A StreamVitals care coordinator will be in contact with you to discuss how our platform works.  
                </div>
             <div><br/>Best Regards,<br/>
                StreamVitals Team
             </div>
            </div>
            </div>
            </section>";
            AlternateView AV =
            AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);

            return AV;
        }

        public async Task<bool> RequestDemoEmail(string Email)
        {
            try
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(Email));
                message.From = new MailAddress(FromMail); 
                message.Subject = FromSubject;
                message.IsBodyHtml = true;
                message.AlternateViews.Add(MailBody());

                using (var smtp = new System.Net.Mail.SmtpClient())
                {
                    smtp.UseDefaultCredentials = true;
                    var credential = new NetworkCredential
                    {
                        UserName = FromMail,
                        Password = FromPassword
                    };
                    smtp.Credentials = credential;
                    smtp.Host = Host;
                    smtp.Port = Port;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private AlternateView MailBody()
        {
            string str = @"
            <section id='content'>
            <div class='container center clearfix>
            <div class='heading-block center'>
                <h1>Thank you for getting in touch! </h1>
                <span>We appreciate you contacting us/ [StreamVitals]. One of our colleagues will get back in touch with you soon!</span>
            </div>
            <p>
                Have a great day!
            </p>
            </div>
            </div>
            </section>";
            AlternateView AV =
            AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);
            return AV;
        }

        public async Task<bool> AppointmentAndRegistrationLink(RequestDemoViewModel objApptEmail, bool IsRegister)
        {
            try
            {
                var split = objApptEmail.Email.Split('@');
                var mail = split[1];
                var data = false;
                if (mail == "gmail.com")
                {
                    data = await GmailInvitation(objApptEmail, IsRegister);
                }
                else
                {
                    data = await OutlookInvitation(objApptEmail, IsRegister);
                }

                return data;
            }
            catch (Exception)
            {

                return false;
            }
        }
        private AlternateView MailBodyForAppointmentAndRegistration(RequestDemoViewModel model)
        {
            string str = @"
            <section id='content'>
            <div class='container center clearfix>
            <div class='heading-block center'>
                <span>Your Appointment has been scheduled on '" + model.Date.ToShortDateString() + @"' at '"+ model.Time+ @"'.</span>
                <span>Please click on following link for Registration</span>
                <br>
                <span>'"+ Utility.Link + "/" + model.DoctorId + "" + @"'</span>
            </div>
            <p>
                Have a great day!
            </p>
            </div>
            </div>
            </section>";
            AlternateView AV =
            AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);
            return AV;
        }

        private AlternateView MailBodyForAppointment(RequestDemoViewModel model)
        {
            string str = @"
            <section id='content'>
            <div class='container center clearfix>
            <div class='heading-block center'>
                <span>Your Appointment has been scheduled on '" + model.Date.ToShortDateString() + @"' at '" + model.Time + @"'.</span>
            </div>
            <p>
                Have a great day!
            </p>
            </div>
            </div>
            </section>";
            AlternateView AV =
            AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);
            return AV;
        }

        public async Task<bool> doctorRegistrationEmail(DoctorViewModel model)
        {
            try
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(model.User.Email));
                message.From = new MailAddress(FromMail); message.Subject = FromSubject;
                message.IsBodyHtml = true;
                message.AlternateViews.Add(MailBodyForDoctorRegistration(model));
                //message.Body= Link + "/" + model.Id + "";

                using (var smtp = new System.Net.Mail.SmtpClient())
                {
                    smtp.UseDefaultCredentials = true;
                    var credential = new NetworkCredential
                    {
                        UserName = FromMail,
                        Password = FromPassword
                    };
                    smtp.Credentials = credential;
                    smtp.Host = Host;
                    smtp.Port = Port;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    //return true;
                }

                if (await sendEmailToSender(message))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private AlternateView MailBodyForDoctorRegistration(DoctorViewModel model)
        {
            string str = @"
            <section id='content'>
            <div class='container center clearfix>
            <div class='heading-block center'>
                <h1>Hi " + model.User.FullName + @",</h1>
                <span>Please click on following link for Registration</span>
                <span>'" + Utility.Link + "/" + model.Id + "" + @"'</span>
            </div>
            <p>
                Have a great day!
            </p>
            </div>
            </div>
            </section>";
            AlternateView AV =
            AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);
            return AV;
        }

        private async Task<bool> sendEmailToSender(MailMessage mail)
        {
            try
            {
                mail.Subject = "Email Sent Successfully!";
               // var senderEmail = System.Web.HttpContext.Current.Session["Email"].ToString();
                var senderEmail = mail.To.ToString();
                if (!string.IsNullOrEmpty(senderEmail))
                {
                    mail.To.Clear();
                    mail.To.Add(new MailAddress(senderEmail));
                    if(await sendEmail(mail))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }

        }
        private async Task<bool> sendEmail(MailMessage message)
        {
            try
            {
                using (var smtp = new System.Net.Mail.SmtpClient())
                {
                    smtp.UseDefaultCredentials = true;
                    var credential = new NetworkCredential
                    {
                        UserName = FromMail,
                        Password = FromPassword
                    };
                    smtp.Credentials = credential;
                    smtp.Host = Host;
                    smtp.Port = Port;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return true;
                }
            }catch(Exception )
            {
                return false;
            }
        }

        public async Task<bool> ForgotPassword(UserViewModel model)
        {
            try
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(model.Email));
                message.From = new MailAddress(FromMail); message.Subject = FromSubject;
                message.IsBodyHtml = true;
                message.AlternateViews.Add(forgotPassword_Body(model));

                using (var smtp = new System.Net.Mail.SmtpClient())
                {
                    smtp.UseDefaultCredentials = false;
                    var credential = new NetworkCredential
                    {
                        UserName = FromMail,
                        Password = FromPassword
                    };
                    smtp.Credentials = credential;
                    smtp.Host = Host;
                    smtp.Port = Port;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        private AlternateView forgotPassword_Body(UserViewModel model)
        {
            string str = @"
        <section id='content'>
            <div class='container center clearfix>
            <div class='heading-block center'>
                 <h2>Hi " + model.FirstName + @",</h2>
                <div>We're sending you this email because you requested a password reset.Your account password is given below.  
                </div>
                <div>
                 <p>Password:" + model.Password + @"</p>
                </div>

             <div><br/>Best Regards,<br/>
                StreamVitals Team
             </div>
            </div>
            </div>
            </section>";
            AlternateView AV =
            AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);

            return AV;
        }



        public async Task<bool> OutlookInvitation(RequestDemoViewModel objApptEmail, bool IsRegister)
        {
            try
            {
                var mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(FromMail);
                mailMessage.To.Add(objApptEmail.Email);
                mailMessage.Subject = FromSubject;
                mailMessage.IsBodyHtml = true;
                if (IsRegister)
                {
                    mailMessage.AlternateViews.Add(MailBodyForAppointment(objApptEmail));
                }
                else
                    mailMessage.AlternateViews.Add(MailBodyForAppointmentAndRegistration(objApptEmail));

                using (var smtpClient = new System.Net.Mail.SmtpClient())
                {

                    smtpClient.Credentials = new NetworkCredential
                    {
                        UserName = FromMail,
                        Password = FromPassword
                    };

                    smtpClient.EnableSsl = true;
                    smtpClient.Host = Host;
                    smtpClient.Port = Port;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;


                    //Invitation
                    DateTime date = objApptEmail.Date;
                    var time = Convert.ToDateTime(objApptEmail.Time);
                    DateTime startTime = time;
                    DateTime endTime = startTime.AddMinutes(60);
                    string now = DateTime.Now.ToString("yyyyMMddTHHmmssZ");
                    StringBuilder str = new StringBuilder();
                    str.AppendLine("BEGIN:VCALENDAR");

                    //PRODID: identifier for the product that created the Calendar object
                    str.AppendLine("PRODID:-//ABC Company//Outlook MIMEDIR//EN");
                    str.AppendLine("VERSION:2.0");
                    str.AppendLine("X-WR-RELCALID:ABC2");
                    str.AppendLine("METHOD:REQUEST");

                    str.AppendLine("BEGIN:VEVENT");

                    str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", startTime));
                    str.AppendLine(string.Format("DTSTAMP:{0:yyyyMMddTHHmmssZ}", DateTime.Now));
                    str.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", endTime));
                    str.AppendLine(string.Format("LOCATION: {0}", objApptEmail.Location));


                    // UID should be unique.
                    str.AppendLine(string.Format("UID:{0}", Guid.NewGuid()));
                    str.AppendLine(string.Format("SEQUENCE:{0}", 0));

                    str.AppendLine(string.Format("DESCRIPTION:{0}", mailMessage.Body));
                    str.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", mailMessage.Body));
                    str.AppendLine(string.Format("SUMMARY:{0}", FromSubject));

                    str.AppendLine("STATUS:CONFIRMED");
                    str.AppendLine("BEGIN:VALARM");
                    str.AppendLine("TRIGGER:-PT15M");
                    str.AppendLine("ACTION:Accept");
                    str.AppendLine("DESCRIPTION:Reminder");
                    str.AppendLine("X-MICROSOFT-CDO-BUSYSTATUS:BUSY");
                    str.AppendLine("END:VALARM");
                    str.AppendLine("END:VEVENT");

                    str.AppendLine(string.Format("ORGANIZER:MAILTO:{0}", mailMessage.From.Address));
                    str.AppendLine(string.Format("ATTENDEE;CN=\"{0}\";RSVP=TRUE:mailto:{1}", mailMessage.To[0].DisplayName, mailMessage.To[0].Address));

                    str.AppendLine("END:VCALENDAR");
                    System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType("text/calendar");
                    ct.Parameters.Add("method", "REQUEST");
                    ct.Parameters.Add("name", "meeting.ics");
                    AlternateView avCal = AlternateView.CreateAlternateViewFromString(str.ToString(), ct);
                    mailMessage.AlternateViews.Add(avCal);
                    //smtpClient.Send(mailMessage);

                }
                if (await sendEmailToSender(mailMessage))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> GmailInvitation(RequestDemoViewModel emailModel, bool IsRegister)
        {
            try
            {
                var mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(FromMail);
                mailMessage.To.Add(emailModel.Email);
                mailMessage.Subject = FromSubject;
                //mailMessage.Body = "mail body";
                mailMessage.IsBodyHtml = true;
                if (IsRegister)
                {
                    mailMessage.AlternateViews.Add(MailBodyForAppointment(emailModel));
                }
                else
                    mailMessage.AlternateViews.Add(MailBodyForAppointmentAndRegistration(emailModel));

                using (var smtpClient = new System.Net.Mail.SmtpClient())
                {
                    smtpClient.Credentials = new NetworkCredential
                    {
                        UserName = FromMail,
                        Password = FromPassword
                    };

                    smtpClient.EnableSsl = true;
                    smtpClient.Host = Host;
                    smtpClient.Port = Port;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;


                    //Invitation
                    DateTime date = emailModel.Date;
                    var time = Convert.ToDateTime(emailModel.Time);
                    DateTime startTime = time;
                    DateTime endTime = startTime.AddMinutes(60);
                    string now = DateTime.Now.ToString("yyyyMMddTHHmmssZ");



                string ics = string.Format(
                    "BEGIN:VCALENDAR\r\n" +
                    "PRODID:-//Comapany & Co.//Their Application//EN\r\n" +
                    "VERSION:2.0\r\n" +
                    "METHOD:REQUEST\r\n" +
                    "BEGIN:VEVENT\r\n" +
                    "CREATED:{0}\r\n" +
                    "DTSTART:{1}\r\n" +
                    "DTEND:{2}\r\n" +
                    "DTSTAMP:{3}\r\n" +
                    "UID:{4}\r\n" +
                    "ATTENDEE;CN=\"{5}\";RSVP=TRUE:mailto:{6}\r\n" +
                    "X-ALT-DESC;FMTTYPE=text/html:{7}\r\n" +
                    "LOCATION:{8}\r\n" +
                    "ORGANIZER;CN=\"streamvitals\":mailto:streamvitalsllc@gmail.com\r\n" +
                    "SEQUENCE:0\r\n" +
                    "SUMMARY:{9}\r\n" +
                    "TRANSP:OPAQUE\r\n" +
                    "END:VEVENT\r\n" +
                    "END:VCALENDAR",
                    now,
                    string.Concat(date.Year, date.Month.ToString("D2"), date.Day.ToString("D2"), "T", startTime.Hour.ToString("D2"), startTime.Minute.ToString("D2"), startTime.Second.ToString("D2")),
                    string.Concat(date.Year, date.Month.ToString("D2"), date.Day.ToString("D2"), "T", endTime.Hour.ToString("D2"), endTime.Minute.ToString("D2"), endTime.Second.ToString("D2")),
                    now,
                    Guid.NewGuid(),
                    emailModel.FirstName,
                     emailModel.Email,
                    "Description",
                    emailModel.Location,
                    "Appointment Schedule");

                var contentType = new System.Net.Mime.ContentType("text/calendar");
                contentType.Parameters.Add("method", "REQUEST");
                contentType.Parameters.Add("name", "invitation.ics");
                byte[] bytes = Encoding.UTF8.GetBytes(ics);
                MemoryStream stream = new MemoryStream(bytes);
                Attachment icsAttachment = new Attachment(stream, "invitation.ics", "text/calendar");

                mailMessage.Attachments.Add(icsAttachment);

                //smtpClient.Send(mailMessage);
            }
                if (await sendEmailToSender(mailMessage))
                {
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}