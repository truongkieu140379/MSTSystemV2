using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Global;

namespace TutorSearchSystem.Filters
{
    //String tutorGender, String educationLevel
    public class FilterQueryDto
    {
        [FromQuery(Name = "tuteeId")]
        public int TuteeId { get; set; }
        [FromQuery(Name = "subjectId")]
        public int SubjectId { get; set; }
        [FromQuery(Name = "classId")]
        public int ClassId { get; set; }
        [FromQuery(Name = "maxFee")]
        public float MaxFee { get; set; } = float.MaxValue;
        [FromQuery(Name = "minFee")]
        public float MinFee { get; set; } = 0.0F;
        [FromQuery(Name = "weekdays")]
        public string Weekdays { get; set; } = "";

        [FromQuery(Name = "beginDate")]
        //fix here: Tools.getUTC()
        public DateTime BeginDate { get; set; } = DateTime.MinValue;
        [FromQuery(Name = "endDate")]
        public DateTime EndDate { get; set; } = DateTime.MaxValue;
        [FromQuery(Name = "minTime")]
        public DateTime MinTime { get; set; } = DateTime.MinValue;
        [FromQuery(Name = "maxTime")]
        public DateTime MaxTime { get; set; } = DateTime.MaxValue;
        [FromQuery(Name = "tutorGender")]
        public String TutorGender { get; set; } = "";
        [FromQuery(Name = "educationLevel")]
        public String EducationLevel { get; set; } = "";
        [FromQuery(Name = "studyForm")]
        public String StudyForm { get; set; } = "";

    }
}
