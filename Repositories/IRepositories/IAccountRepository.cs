using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Models;

namespace TutorSearchSystem.Repositories.IRepositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account> Get(String email);
        Task<bool> IsEmailExist(String email);
        Task UpdateAccount(Account account);
        
    }
}
