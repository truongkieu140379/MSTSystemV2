using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models
{
    public class Feedback 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Tutee")]
        public int TuteeId { get; set; }
        public virtual Tutee Tutee { get; set; }
        [ForeignKey("Tutor")]
        public int TutorId { get; set; }
        public virtual Tutor Tutor { get; set; }
        [MaxLength(500, ErrorMessage = "Content must be less than 500 characters.")]
        public string Comment { get; set; }
        [Required]
        [Range(0, 5, ErrorMessage = "Rate must be 1 - 5.")]
        public int Rate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        [ForeignKey("Manager")]
        public Nullable<int> ConfirmedBy { get; set; }
        public virtual Manager Manager { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> ConfirmedDate { get; set; } 
        [Required]
        [MaxLength(20, ErrorMessage = "Status must be less than 20 characters.")]
        public string Status { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
