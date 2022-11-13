using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models.ExtendedModels
{
    public class ExtendedCourse : Course
    {
        public string SubjectName { get; set; }
        public string ClassName { get; set; }
        public DateTime? FollowDate { get; set; }
        public string EnrollmentStatus { get; set; }
        public int EnrollmentId { get; set; }
        public int TuteeId { get; set; }
        public int NumberOfTutee { get; set; }
        public string TutorAvatarUrl { get; set; }
        public string TutorName { get; set; }
        public string TutorEmail { get; set; }
        public string ConfirmerName { get; set; }
        public string TutorAddress { get; set; }
        public int AvailableSlot { get; set; }
        public bool? IsTakeFeedback { get; set; }
    }
}
