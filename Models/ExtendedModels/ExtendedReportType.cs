using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models.ExtendedModels
{
    public class ExtendedReportType : ReportType
    {
        public string UpdatorName { get; set; }
        public string CreatorName { get; set; }
        public string RoleName { get; set; }
    }
}
