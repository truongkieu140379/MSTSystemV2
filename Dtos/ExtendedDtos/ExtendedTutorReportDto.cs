using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Dtos.ExtendedDtos
{
    public class ExtendedTutorReportDto : TutorReportDto
    {
        public string ConfirmName { get; set; }
        public string ReportName { get; set; }
        public string TutorName { get; set; }
        public string TutorEmail { get; set; }
    }
}
