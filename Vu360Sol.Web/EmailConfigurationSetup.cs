using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Vu360Sol.ViewModel.Common;

namespace Vu360Sol.Web
{
    public class EmailConfigurationSetup
    {
        private EmailConfigurationSetup() { }

        private static  EmailConfigurationViewModel _configration;

        public static EmailConfigurationViewModel GetConfigration()
        {
            if(_configration == null)
            {
                using (var client = Utility.GetHttpClient())
                {
                    var responseTask = client.GetAsync("EmailConfiguration/Get/");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    var readTask = result.Content.ReadAsAsync<EmailConfigurationViewModel>();
                    readTask.Wait();
                    _configration = readTask.Result;
                }
            }
            return _configration;
        }
    }
}