using System;
using TutorSearchSystem.Global;

namespace TutorSearchSystem.Dtos
{
    public class TutorDto : UserBaseDto
    {
        public int Id { get; set; }
        public string EducationLevel { get; set; }
        public string School { get; set; }
        public int Points { get; set; }
        public int MembershipId { get; set; }
        public string SocialIdUrl { get; set; }
        public Nullable<int> ConfirmedBy { get; set; } = null;
        public Nullable<DateTime> ConfirmedDate { get; set; } = null;
        public DateTime CreatedDate { get; set; } = Tools.GetUTC();
    }
}
