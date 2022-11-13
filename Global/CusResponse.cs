using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Global
{
    public class CusResponse
    {
        public bool Status { get; set; } = false;
        public string Message { get; set; } = "";
        public string Type { get; set; }
    }
}
