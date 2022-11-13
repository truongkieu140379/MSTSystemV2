using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models
{
    public class Account 
    {
        [Key]
        [MaxLength(256, ErrorMessage = "Email must be less than 256 characters.")]
        [EmailAddress(ErrorMessage = "Email address is not valid.")]
        public string Email { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public string TokenNotification { get; set; } = "";
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
