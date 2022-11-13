using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models
{
    public class Tutor : UserBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "EducationLevel name must be less than 100 characters.")]
        public string EducationLevel { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "School name must be less than 100 characters.")]
        public string School { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Points must be a positive float number.")]
        public int Points { get; set; }
        [ForeignKey("Membership")]
        public int MembershipId { get; set; }
        public virtual Membership Membership { get; set; }
        [Required]
        [Url]
        public string SocialIdUrl { get; set; }
        [ForeignKey("Manager")]
        public Nullable<int> ConfirmedBy { get; set; }
        public virtual Manager TutorConfirmer { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> ConfirmedDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<TutorTransaction> TutorTransactions { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<TutorReport> TutorReports { get; set; }
    }
}
