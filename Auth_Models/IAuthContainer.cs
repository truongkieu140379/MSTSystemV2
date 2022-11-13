using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TutorSearchSystem.Auth_Models
{
    public interface IAuthContainer
    {
        string SecretKey { get; set; }
        string SecurityAlgorithm { get; set; }
        Claim[] Claims { get; set; }
    }
}
