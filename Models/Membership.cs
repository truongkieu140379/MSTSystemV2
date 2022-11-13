using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models
{
    public class Membership : DescriptionBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Membership Name must be less than 100 characters.")]
        public string Name { get; set; }
        [Required]
        [Range(0.0, 1.0, ErrorMessage = "PointRate must be a positive float number between 0.0-1.0")]
        public float PointRate { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "PointAmount must be a positive integer.")]
        public int PointAmount { get; set; }
        [ForeignKey("Manager")]
        public Nullable<int> UpdatedBy { get; set; }
        public virtual Manager MembershipUpdater { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> UpdatedDate { get; set; }
        [ForeignKey("Manager")]
        public int CreatedBy { get; set; }
        public virtual Manager MembershipCreator { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        public ICollection<Tutor> Tutors { get; set; }
    }
}
