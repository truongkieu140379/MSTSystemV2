using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models.ExtendedModels
{
    public class ExtendedEnrollment : Enrollment
    {
        public String CourseName { get; set; }
        public float StudyFee { get; set; }
        public string  TuteeName { get; set; }
    }
}
