using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Dtos.ExtendedDtos
{
    public class ExtendedTutorTransactionDto : TutorTransactionDto
    {
        public string FeeName { get; set; }
        public string CourseName { get; set; }
    }
}
