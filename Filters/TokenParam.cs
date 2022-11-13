using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TutorSearchSystem.Filters
{
    public class TokenParam
    {
        public string Email { get; set; } = "";
        public string Token { get; set; } = "";
    }
}
