using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Global;

namespace TutorSearchSystem.Dtos
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; } = Tools.GetUTC();
        //public string SendFromUser { get; set; }
        public string SendToUser { get; set; }
        public bool IsRead { get; set; } = false;
    }
}

