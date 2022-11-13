using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Dtos.ExtendedDtos
{
    //this course model contain tutor information
    public class CourseTutorDto : CourseDto
    {
        public string ConfirmerName { get; set; }
        public string Fullname { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AvatarImageLink { get; set; }
        public string Address { get; set; }
        public int RoleId { get; set; }
        public double AverageRatingStar { get; set; }
        public float Distance { get; set; }
        public int AvailableSlot { get; set; }
        public int SubjectId { get; set; }
    }
}
