using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Dtos
{
    public class UserBaseDto : DescriptionBaseDto
    {
        public string Fullname { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AvatarImageLink { get; set; }
        public string Address { get; set; }
        public int RoleId { get; set; }
    }
}
