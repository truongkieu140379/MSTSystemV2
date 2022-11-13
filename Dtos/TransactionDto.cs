using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Dtos
{
    public class TransactionDto : DescriptionBaseDto
    {
        public DateTime DateTime { get; set; }
        public float TotalAmount { get; set; }
        public float Amount { get; set; }
    }
}
