using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models
{
    public class Course : DescriptionBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "subject name must be less than 100 characters.")]
        public string Name { get; set; }
        //begin time of the course
        [Required]
        [DataType(DataType.Time, ErrorMessage = "Begintime must be format hh:mm:ss")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime BeginTime { get; set; }
        [Required]
        [DataType(DataType.Time, ErrorMessage = "EndTime must be format hh:mm:ss")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }
        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "StudyFee Price must be a positive float number.")]
        public float StudyFee { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "DaysInWeek must be less than 100 characters.")]
        public string DaysInWeek { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BeginDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        [ForeignKey("ClassHasSubject")]
        public int ClassHasSubjectId { get; set; }
        public virtual ClassHasSubject ClassHasSubject { get; set; }
        
        [ForeignKey("Manager")]
        public Nullable<int> ConfirmedBy { get; set; }
        public virtual Manager Manager { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> ConfirmedDate { get; set; }
        [ForeignKey("Tutor")]
        public int CreatedBy { get; set; }
        public virtual Tutor Tutor { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        [Required]
        public int MaxTutee { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Location must be less than 500 characters.")]
        public string Location { get; set; }
        public string ExtraImages { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        
        [MaxLength(2048, ErrorMessage = "Precondition must be less than 2048 characters.")]
        public string Precondition { get; set; }
        public ICollection<CourseDetail> CourseDetails { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }

    }
}
