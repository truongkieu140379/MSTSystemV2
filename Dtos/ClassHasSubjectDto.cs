using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Global;

namespace TutorSearchSystem.Dtos
{
    public class ClassHasSubjectDto : DescriptionBaseDto
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; } = Tools.GetUTC();
    }
}
