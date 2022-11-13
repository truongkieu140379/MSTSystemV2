using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models
{
    public class Class : DescriptionBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Class name must be less than 100 characters.")]
        public string Name { get; set; }
        [Required]
        [ForeignKey("Manager")]
        public int UpdatedBy { get; set; }
        public virtual Manager ClassUpdater { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedDate { get; set; }
        [Required]
        [ForeignKey("Manager")]
        public int CreatedBy { get; set; }
        public virtual Manager ClassCreator { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        public ICollection<ClassHasSubject> ClassHasSubjects { get; set; }
    }
}
