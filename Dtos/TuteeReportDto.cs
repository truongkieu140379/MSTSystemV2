using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Global;

namespace TutorSearchSystem.Dtos
{
    public class TuteeReportDto : DescriptionBaseDto
    {
        public int Id { get; set; }
        public int ReportTypeId { get; set; }
        public int EnrollmentId { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; } = Tools.GetUTC();
        public Nullable<int> ConfirmedBy { get; set; } = null;
        public Nullable<DateTime> ConfirmedDate { get; set; } = null;
    }
}
