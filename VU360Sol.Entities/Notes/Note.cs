using System;
using System.Collections.Generic;
using System.Text;
using VU360Sol.Entities.Common;
using VU360Sol.Entities.Doctors;
using VU360Sol.Entities.RequestDemoes;

namespace VU360Sol.Entities.Notes
{
    public class Note : BaseEntity
    {
        public int ReferenceId { get; set; }
        public string Discription { get; set; }
        public int RefrenceTableId { get; set; }
        public RefrenceTable RefrenceTable { get; set; }

    }
}
