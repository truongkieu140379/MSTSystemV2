using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Filters
{
    public class ClassParameter :QueryStringParameter
    {
        public string Name { get; set; } = "";
        public string Status { get; set; } = "";
    }
}
