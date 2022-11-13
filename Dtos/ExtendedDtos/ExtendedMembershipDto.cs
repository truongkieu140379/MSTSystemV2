using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Dtos.ExtendedDtos
{
    public class ExtendedMembershipDto : MembershipDto
    {
        public string CreatorFullname { get; set; }
        public string UpdaterFullname { get; set; }
    }
}
