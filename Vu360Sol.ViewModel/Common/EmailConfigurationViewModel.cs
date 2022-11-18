using System;
using System.Collections.Generic;
using System.Text;

namespace Vu360Sol.ViewModel.Common
{
    public class EmailConfigurationViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Host { get; set; }
        public int Port { get; set; }
    }
}
