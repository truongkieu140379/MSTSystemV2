using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Dtos.ExtendedDtos
{
    public class ExtendedEnrollmentDto : EnrollmentDto
    {
        public String CourseName { get; set; }
        public float StudyFee { get; set; }
        public string TuteeName { get; set; }
    }
}
