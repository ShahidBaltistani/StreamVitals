using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Vu360Sol.Web
{
    public class Utility
    {
        public const int PageSize = 5;
        internal const int SessionTimeout = 20;

        // API

        internal const string APIBaseAddress = "http://localhost:49891";
         // internal const string APIBaseAddress = "http://SVAPI.vu360solutions.org/";

        // Registration Link for Email

        internal const string Link = "https://localhost:44313/Registration/DoctorRegistration";
       // internal const string Link = "http://streamvitals.com/Registration/DoctorRegistration";

        public static HttpClient GetHttpClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(APIBaseAddress) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (System.Web.HttpContext.Current.Session["UserToken"] != null)
            {
                client.DefaultRequestHeaders.Authorization =
               new AuthenticationHeaderValue("Bearer", HttpContext.Current.Session["UserToken"].ToString());
            }

            return client;
        }
        public static DateTime getDefaultTime()
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));
        }

    }
}