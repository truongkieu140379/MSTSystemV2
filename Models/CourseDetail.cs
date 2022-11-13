using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TutorSearchSystem.Models
{

    public class CourseDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course {get;set;}
        [MaxLength(250, ErrorMessage = "Period must be less than 250 characters.")]
        public string Period { get; set; }
        [MaxLength(500, ErrorMessage = "Schedule must be less than 500 characters.")]
        public string Schedule { get; set; }

        [MaxLength(2048, ErrorMessage = "Learning Outcome must be less than 2048 characters.")]
        public string LearningOutcome { get; set; }
    }
}
