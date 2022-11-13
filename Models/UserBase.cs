using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models
{
    public class UserBase : DescriptionBase
    {
        [Required]
        [MaxLength(256, ErrorMessage = "Fullname must be less than 256 characters.")]
        public string  Fullname { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Gender must be less than 10 characters.")]
        public string Gender { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }
        [Required]
        [MaxLength(256, ErrorMessage = "Email must be less than 256 characters.")]
        [EmailAddress(ErrorMessage = "Email address is not valid.")]
        public string Email { get; set; }
        [Required]
        [Phone(ErrorMessage = "Phone number is not valid.")]
        [MaxLength(16, ErrorMessage = "Phone must be less than 16 characters.")]
        public string Phone { get; set; }
        [Required]
        [Url(ErrorMessage = "AvatarImageLink must follow Url format.")]
        public string AvatarImageLink { get; set; }
        [Required]
        [MaxLength(500, ErrorMessage = "Address must be less than 500 characters.")]
        public string Address { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

    }
}
