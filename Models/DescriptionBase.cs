using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models
{
    public class DescriptionBase
    {
        [MaxLength(500, ErrorMessage = "Description must be less than 500 characters.")]
        public string Description { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Status must be less than 20 characters.")]
        public string Status { get; set; }
    }
}
