using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Global;

namespace TutorSearchSystem.Models
{
    public class TutorUpdateProfile
    {
        public int Id { get; set; }
        [MaxLength(256, ErrorMessage = "Fullname must be less than 256 characters.")]
        public string Fullname { get; set; }
        [MaxLength(10, ErrorMessage = "Gender must be less than 10 characters.")]
        public string Gender { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> Birthday { get; set; }
        [Phone(ErrorMessage = "Phone number is not valid.")]
        public string Phone { get; set; }
        [Url(ErrorMessage = "AvatarImageLink must follow Url format.")]
        public string AvatarImageLink { get; set; }
        [MaxLength(500, ErrorMessage = "Address must be less than 500 characters.")]
        public string Address { get; set; }
        [MaxLength(100, ErrorMessage = "EducationLevel name must be less than 100 characters.")]
        public string EducationLevel { get; set; }
        [MaxLength(100, ErrorMessage = "School name must be less than 100 characters.")]
        public string School { get; set; }
        [Url]
        public string SocialIdUrl { get; set; }
        public string CertificateImages { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; } 
        public string Description { get; set; }
        public string Status { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> ConfirmedDate { get; set; }
        public Nullable<int> Points { get; set; }
        public Nullable<int> MembershipId { get; set; }
        public Nullable<int> RoleId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RequestUpdateDate { get; set; }

    }
}

