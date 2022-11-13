using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Models;
using TutorSearchSystem.Repositories.IRepositories;

namespace TutorSearchSystem.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        //private readonly TSDbContext _context;

        public RoleRepository(TSDbContext context) : base(context)
        {
        }
    }
}
