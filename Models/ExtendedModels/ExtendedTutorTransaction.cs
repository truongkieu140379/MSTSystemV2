using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models.ExtendedModels
{
    public class ExtendedTutorTransaction : TutorTransaction
    {
        public string FeeName { get; set; }
        public string CourseName { get; set; }
    }
}
