using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models
{
    public class TutorReport : DescriptionBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("ReportType")]
        public int ReportTypeId { get; set; }
        public virtual ReportType ReportType { get; set; }
        [ForeignKey("Tutor")]
        public int TutorId { get; set; }
        public virtual Tutor Tutor { get; set; }
        public string Image { get; set; }
        [ForeignKey("Manager")]
        public Nullable<int> ConfirmedBy { get; set; }
        public virtual Manager Manager { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> ConfirmedDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
    }
}
