using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Global;

namespace TutorSearchSystem.Dtos
{
    public class ReportTypeDto : DescriptionBaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public Nullable<int> UpdatedBy { get; set; } = null;
        public Nullable<DateTime> UpdatedDate { get; set; } = null;
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = Tools.GetUTC();
    }
}
