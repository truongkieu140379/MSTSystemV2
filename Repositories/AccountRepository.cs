using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Models;
using TutorSearchSystem.Repositories.IRepositories;

namespace TutorSearchSystem.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly TSDbContext _context;

        public AccountRepository(TSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Account> Get(string email)
            
        {
            
            return await _context.Account.Where(a => a.Email == email).FirstOrDefaultAsync();
        }

        public async Task<bool> IsEmailExist(string email)
        {
            return await _context.Account.AnyAsync( a => a.Email == email);
        }

        public async Task UpdateAccount(Account account)
        {
            _context.Entry(account).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
       
    }
}
