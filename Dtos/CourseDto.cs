using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Global;

namespace TutorSearchSystem.Dtos
{
    public class CourseDto :DescriptionBaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public float StudyFee { get; set; }
        public string DaysInWeek { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ClassHasSubjectId { get; set; }
        public Nullable<int> ConfirmedBy { get; set; } = null;
        public Nullable<DateTime> ConfirmedDate { get; set; } = null;
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = Tools.GetUTC();
        public int MaxTutee { get; set; }
        public string Location { get; set; }
        public string ExtraImages { get; set; }
        public string Precondition { get; set; }
    }
}
