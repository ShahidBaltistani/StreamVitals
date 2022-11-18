using System;
using System.Collections.Generic;
using System.Text;
using VU360Sol.Entities.Common;

namespace VU360Sol.Entities.Searching
{
    public class Searching : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keyword { get; set; }
        public string Paragraph { get; set; }
        public string Href { get; set; }
    }
}
