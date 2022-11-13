using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Global;

namespace TutorSearchSystem.Dtos
{
    public class ClassDto : DescriptionBaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; } = Tools.GetUTC();
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = Tools.GetUTC();
    }
}
