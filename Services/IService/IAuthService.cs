using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Auth_Models;
using TutorSearchSystem.TokenSettings;

namespace TutorSearchSystem.Services.IService
{
    public interface IAuthService
    {
        Task<TSTokenSetting> Authenticate(string email);
    }
}
