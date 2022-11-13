using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Filters
{
    public class TuteeParameter: QueryStringParameter
    {
        public string TuteeName { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
