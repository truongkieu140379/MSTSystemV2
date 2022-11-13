using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models
{
    public class Manager : UserBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        [Required]
        [ForeignKey("Manager")]
        public int CreatedBy { get; set; }
        public virtual Manager ManagerCreator { get; set; }
        //
        [InverseProperty("ClassUpdater")]
        public ICollection<Class> UpdatedClasses { get; set; }
        [InverseProperty("ClassCreator")]
        public ICollection<Class> CreatedClasses { get; set; }
        //
        public ICollection<Course> Courses { get; set; }
        //
        [InverseProperty("FeeUpdater")]
        public ICollection<Fee> UpdatedFees { get; set; }
        [InverseProperty("FeeCreator")]
        public ICollection<Fee> CreatedFees { get; set; }
        //membership
        [InverseProperty("MembershipUpdater")]
        public ICollection<Membership> UpdatedMemberships { get; set; }
        [InverseProperty("MembershipCreator")]
        public ICollection<Membership> CreatedMemberships { get; set; }
        //
        [InverseProperty("SubjectUpdater")]
        public ICollection<Subject> UpdatedSubjects { get; set; }
        [InverseProperty("SubjectCreator")]
        public ICollection<Subject> CreatedSubjects { get; set; }
        //
        [InverseProperty("SubjectManager")]
        public ICollection<Subject> ManagedSubjects { get; set; }
        //
        public ICollection<Feedback> Feedbacks { get; set; }
        //
        [InverseProperty("ClassHasSubjectCreator")]
        public ICollection<ClassHasSubject> CreatedClassHasSubjects { get; set; }
        //
        [InverseProperty("TutorConfirmer")]
        public ICollection<Tutor> ConfirmedTutors { get; set; }
        //
        [InverseProperty("ManagerCreator")]
        public ICollection<Manager> CreatedManagers { get; set; }
        //for report type
        [InverseProperty("ReportTypeUpdater")]
        public ICollection<ReportType> UpdatedReportTypes { get; set; }
        [InverseProperty("ReportTypeCreator")]
        public ICollection<ReportType> CreatedReportTypes { get; set; }
        //
        [InverseProperty("Manager")]
        public ICollection<TuteeReport> TuteeReports { get; set; }
        //
        [InverseProperty("Manager")]
        public ICollection<TutorReport> TutorReports { get; set; }
    }
}
