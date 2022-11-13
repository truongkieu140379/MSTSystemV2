using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Models;
using TutorSearchSystem.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace TutorSearchSystem.Repositories
{
    public class TutorUpdateProfileRepository : GenericRepository<TutorUpdateProfile>, ITutorUpdateProfileRepository
    {

        private readonly TSDbContext _context;
        public TutorUpdateProfileRepository(TSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            var entity = await _context.TutorUpdateProfile.FindAsync(id);
            _context.TutorUpdateProfile.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TutorUpdateProfile> GetByEmail(string email)
        {
            return await _context.TutorUpdateProfile.Where(t => t.Email == email).FirstOrDefaultAsync();
        }
    }
}
