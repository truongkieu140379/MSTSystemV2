using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Filters
{
    public class SubjectParameter: QueryStringParameter
    {
        public string SubjectName { get; set; } = "";
        public string ManagerName { get; set; } = "";
        public string Status { get; set; } = "";

    }
}
