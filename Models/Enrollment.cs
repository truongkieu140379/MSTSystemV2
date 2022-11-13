using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models
{
    public class Enrollment : DescriptionBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Tutee")]
        public int TuteeId { get; set; }
        public virtual Tutee Tutee { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        [Required]
        public bool IsTakeFeedback { get; set; } = false;

        //
        public ICollection<TuteeReport> TuteeReports { get; set; }
    }
}
