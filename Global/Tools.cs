using Nancy.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TutorSearchSystem.Global
{
    public static class Tools
    {
        //current universal coordinated(UTC) date and time
        public static DateTime GetUTC()
        {
            Console.WriteLine("this is datetime: " + DateTime.UtcNow);
            return DateTime.UtcNow.AddHours(7);
        }

        public static DateTime GetCurrentDate()
        {
            DateTime utcDateTime = DateTime.UtcNow;
            string vnTimeZoneKey = "SE Asia Standard Time";
            TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);

            DateTime currentDate = DateTime.Parse(TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone).ToShortDateString());
            return currentDate;
        }

    }
}
