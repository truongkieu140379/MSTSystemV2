using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models.ExtendedModels
{
    public class ExtendedSubject : Subject
    {
        public string ManagerName { get; set; }
        public string CreatorName { get; set; }
        public string UpdaterName { get; set; }
    }
}
