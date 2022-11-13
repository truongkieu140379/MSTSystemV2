using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Filters
{
    public class CourseParameter: QueryStringParameter
    {
        public int ManagerId { get; set; } = 0;
        public string SubjectName { get; set; } = "";
        public string CourseName { get; set; } = "";
        public string TutorName { get; set; } = "";
        public string Status { get; set; } = "";
        public DateTime FromDate { get; set; } = new DateTime(0001, 1, 1);
        public DateTime ToDate { get; set; } = DateTime.Today;
        public string SortOrder { get; set; } = "date_desc";

    }
}
