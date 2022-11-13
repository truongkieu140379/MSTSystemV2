using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Global;

namespace TutorSearchSystem.Dtos
{
    public class TutorUpdateProfileDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; } = null;
        public string Gender { get; set; } = null;
        public Nullable<DateTime> Birthday { get; set; } = Tools.GetUTC();
        public string Phone { get; set; } = null;
        public string AvatarImageLink { get; set; } = null;
        public string Address { get; set; } = null;
        public string EducationLevel { get; set; } = null;
        public string School { get; set; } = null;
        public string SocialIdUrl { get; set; } = null;
        public string CertificateImages { get; set; } = null;
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public DateTime ConfirmedDate { get; set; } = Tools.GetUTC();
        public Nullable<int> Points { get; set; }
        public Nullable<int> MembershipId { get; set; }
        public Nullable<int> RoleId { get; set; }
        public DateTime RequestUpdateDate { get; set; } = Tools.GetUTC();
    }
}
