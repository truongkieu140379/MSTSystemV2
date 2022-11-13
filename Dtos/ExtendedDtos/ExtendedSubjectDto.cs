using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Dtos.ExtendedDtos
{
    public class ExtendedSubjectDto : SubjectDto
    {
        public string ManagerName { get; set; }
        public string CreatorName { get; set; }
        public string UpdaterName { get; set; }
    }
}
