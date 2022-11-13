using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models.ExtendedModels
{
    public class ExtendedMembership : Membership
    {
        public string CreatorFullname { get; set; }
        public string UpdaterFullname { get; set; }
    }
}
