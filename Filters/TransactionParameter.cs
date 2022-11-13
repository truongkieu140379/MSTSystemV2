using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TutorSearchSystem.Filters
{
    public class TransactionParameter
    {
        [FromQuery(Name = "fromDate")]
        public DateTime FromDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1);
        [FromQuery(Name = "toDate")]
        public DateTime ToDate { get; set; } = DateTime.Now;
    }
}
