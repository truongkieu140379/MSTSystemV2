using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorSearchSystem.Dtos
{
    public class AccountDto
    {
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string TokenNotification { get; set; } = "";
    }
}
