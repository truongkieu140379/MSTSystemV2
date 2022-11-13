using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models.ExtendedModels
{
    public class ExtendedFeedback : Feedback
    {
        public string TutorName { get; set; }
        public string TuteeName { get; set; }
        public string TuteeAvatarUrl { get; set; }
        public string CourseName { get; set; }
        public string SubjectName { get; set; }
        public string ClassName { get; set; }
    }
}
