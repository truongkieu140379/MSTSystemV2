using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Dtos
{
    public class TutorTransactionDto : TransactionDto
    {
        public int Id { get; set; }
        public int ArchievedPoints { get; set; }
        public int UsedPoints { get; set; }
        //[ForeignKey("Fee")]
        public int FeeId { get; set; }
        //[ForeignKey("Tutor")]
        public int TutorId { get; set; }
        public float FeePrice { get; set; }
        public int CourseId { get; set; }
    }
}
