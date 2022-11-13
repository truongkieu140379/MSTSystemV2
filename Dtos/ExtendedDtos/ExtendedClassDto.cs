using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Dtos.ExtendedDtos
{
    public class ExtendedClassDto : ClassDto
    {
        public string CreatorName { get; set; }
        public string UpdaterName { get; set; }
    }
}
