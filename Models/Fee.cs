using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models
{
    public class Fee : DescriptionBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Class name must be less than 100 characters.")]
        public string Name { get; set; }
        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Price must be a positive float number.")]
        public float Price { get; set; }
        [ForeignKey("Manager")]
        public Nullable<int> UpdatedBy { get; set; }
        public virtual Manager FeeUpdater { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> UpdatedDate { get; set; }
        [ForeignKey("Manager")]
        public int CreatedBy { get; set; }
        public virtual Manager FeeCreator { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        public ICollection<TutorTransaction> TutorTransactions { get; set; }
    }
}
