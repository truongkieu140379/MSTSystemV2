using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TutorSearchSystem.Filters
{
    public class ReportTypeParameter : QueryStringParameter
    {
        [FromQuery(Name = "name")]
        public string Name { get; set; } = "";
        [FromQuery(Name = "status")]
        public string Status { get; set; } = "";
        [FromQuery(Name = "roleId")]
        public int RoleId { get; set; } = 0;
    }
}
