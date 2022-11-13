using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Global;

namespace TutorSearchSystem.Dtos
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public int TuteeId { get; set; }
        public int TutorId { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }
        public DateTime CreatedDate { get; set; } = Tools.GetUTC();
        public Nullable<int> ConfirmedBy { get; set; } = null;
        public Nullable<DateTime> ConfirmedDate { get; set; } = null;
        public string Status { get; set; }
        public int CourseId { get; set; }
    }
}
