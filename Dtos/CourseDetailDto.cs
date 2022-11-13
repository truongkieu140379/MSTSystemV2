using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Dtos
{
    public class CourseDetailDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Period { get; set; }
        public string Schedule { get; set; }
        public string LearningOutcome { get; set; }
    }
}
