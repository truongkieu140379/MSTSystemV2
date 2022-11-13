using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models
{
    public class Transaction : DescriptionBase
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }
        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "TotalAmount must be a positive float number.")]
        public float TotalAmount { get; set; }
        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Amount must be a positive float number.")]
        public float Amount { get; set; }
        
    }
}
