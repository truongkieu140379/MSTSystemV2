    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Global;

namespace TutorSearchSystem.Dtos
{
    public class TuteeDto : UserBaseDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = Tools.GetUTC();
    }
}
