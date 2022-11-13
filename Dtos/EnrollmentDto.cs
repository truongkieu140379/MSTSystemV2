using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Global;

namespace TutorSearchSystem.Dtos
{
    public class EnrollmentDto : DescriptionBaseDto
    {
        public int Id { get; set; }
        public int TuteeId { get; set; }
        public int CourseId { get; set; }
        public DateTime CreatedDate { get; set; } = Tools.GetUTC();
        public bool IsTakeFeedback { get; set; } = false;
    }
}
