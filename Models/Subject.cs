using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models
{
    public class Subject : DescriptionBase
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Subject name must be less than 100 characters.")]
        public string Name { get; set; }
        [Required]
        [ForeignKey("Manager")]
        public int UpdatedBy { get; set; }
        public virtual Manager SubjectUpdater { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedDate { get; set; }
        [ForeignKey("Manager")]
        public int CreatedBy { get; set; }
        public virtual Manager SubjectCreator { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        [ForeignKey("Manager")]
        public int ManageBy { get; set; }
        public virtual Manager SubjectManager { get; set; }

        public ICollection<ClassHasSubject> ClassHasSubjects { get; set; }
    }
}
