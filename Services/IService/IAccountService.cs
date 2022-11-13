using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;

namespace TutorSearchSystem.Services.IService
{
    public interface IAccountService : IBaseService<AccountDto>
    {
        Task<AccountDto> Get(String email);
        Task<bool> IsEmailExist(String email);
        Task ResetToken(string email, string token);
    }
}
