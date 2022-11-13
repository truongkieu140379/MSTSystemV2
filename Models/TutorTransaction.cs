using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models
{
    public class TutorTransaction : Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "ArchievedPoints must be a positive integer.")]
        public int ArchievedPoints { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "UsedPoints must be a positive integer.")]
        public int UsedPoints { get; set; }
        [ForeignKey("Fee")]
        public int FeeId { get; set; }
        public virtual Fee Fee { get; set; }
        [ForeignKey("Tutor")]
        public int TutorId { get; set; }
        public virtual Tutor Tutor { get; set; }
        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Price must be a positive float number.")]
        public float FeePrice { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
