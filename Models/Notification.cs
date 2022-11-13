using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "role name must be less than 100 characters.")]
        public string Title { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "role name must be less than 100 characters.")]
        public string Message { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        [ForeignKey("Account")]
        [EmailAddress(ErrorMessage = "Email address is not valid.")]
        public string SendToUser { get; set; }
        public virtual Account Account { get; set; }
        [Required]
        public bool IsRead { get; set; } = false;
    }
}
