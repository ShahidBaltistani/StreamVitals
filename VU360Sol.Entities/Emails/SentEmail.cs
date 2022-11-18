using System;
using System.Collections.Generic;
using System.Text;

namespace VU360Sol.Entities.Emails
{
    public class SentEmail
    {
        public int Id { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string Header { get; set; }
        public string body { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Reason { get; set; }

    }
}
