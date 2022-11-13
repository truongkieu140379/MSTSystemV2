using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models.ExtendedModels
{
    public class ExtendedTutorReport : TutorReport
    {
        public string ConfirmName { get; set; }
        public string TutorName { get; set; }
        public string ReportName { get; set; }
        public string TutorEmail { get; set; }
    }
}
