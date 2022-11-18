using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VU360Sol.API
{
    public static class Utility
    {
        public static DateTime getDefaultTime()
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));
        }
    }
}
