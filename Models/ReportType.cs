using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models
{
    public class ReportType : DescriptionBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(256, ErrorMessage = "Report type name must be less than 100 characters.")]
        public string Name { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        [ForeignKey("Manager")]
        public Nullable<int> UpdatedBy { get; set; }
        public virtual Manager ReportTypeUpdater { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> UpdatedDate { get; set; }
        [ForeignKey("Manager")]
        public int CreatedBy { get; set; }
        public virtual Manager ReportTypeCreator { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        //
        public ICollection<TuteeReport> TuteeReports { get; set; }
        public ICollection<TutorReport> TutorReports { get; set; }
    }
}
