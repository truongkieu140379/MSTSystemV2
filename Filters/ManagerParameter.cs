using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Filters
{
    public class ManagerParameter : QueryStringParameter
    {
        public string ManagerName { get; set; } = "";
        public string Status { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
