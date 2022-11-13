using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Dtos
{
    public class ImageDto
    {
        public int Id { get; set; }
        public string ImageLink { get; set; }
        public string ImageType { get; set; }
        public string OwnerEmail { get; set; }

    }
}
