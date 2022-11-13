using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Dtos
{
    public class MembershipDto : DescriptionBaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float PointRate { get; set; }
        public int PointAmount { get; set; }
        public Nullable<int> UpdatedBy { get; set; } = null;
        public Nullable<DateTime> UpdatedDate { get; set; } = null;
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
