using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Models
{
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Url(ErrorMessage = "ImageLink must follow Url format.")]
        public string ImageLink { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Image type must be less than 100 characters.")]
        public string ImageType { get; set; }
        [ForeignKey("Account")]
        [EmailAddress(ErrorMessage = "Email address is not valid.")]
        public string OwnerEmail { get; set; }
        public virtual Account Account { get; set; }



    }
}
