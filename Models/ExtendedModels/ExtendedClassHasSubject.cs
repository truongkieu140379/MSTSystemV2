using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models.ExtendedModels
{
    public class ExtendedClassHasSubject : ClassHasSubject
    {
        public string ClassName { get; set; }
        public string SubjectName { get; set; }
        public string ManagerName { get; set; }
    }
}
