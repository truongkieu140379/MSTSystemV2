using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models
{
    public class Role : DescriptionBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "role name must be less than 100 characters.")]
        public string Name { get; set; }

        public ICollection<Account> Accounts { get; set; }
        public ICollection<Tutor> Tutors { get; set; }
        public ICollection<Tutee> Tutees { get; set; }
        public ICollection<ReportType> ReportTypes { get; set; }
    }
}
