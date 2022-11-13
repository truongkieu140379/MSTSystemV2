using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Dtos.ExtendedDtos
{
    public class ExtendedReportTypeDto : ReportTypeDto
    {
        public string CreatorName { get; set; }
        public string UpdatorName { get; set; }
        public string RoleName { get; set; }
    }
}
