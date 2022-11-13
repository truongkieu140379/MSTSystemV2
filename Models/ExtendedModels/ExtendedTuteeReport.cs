using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models.ExtendedModels
{
    public class ExtendedTuteeReport : TuteeReport
    {
        public string TuteeName { get; set; }
        public string ConfirmName { get; set; }
        public string CourseName { get; set; }
        public string ReportName { get; set; }
        public string TutorName { get; set; }
        public string TuteeEmail { get; set; }
        public string TutorEmail { get; set; }
        public int CourseId { get; set; }
    }
}
