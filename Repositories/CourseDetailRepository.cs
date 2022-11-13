using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Filters;
using TutorSearchSystem.Global;
using TutorSearchSystem.Models;
using TutorSearchSystem.Models.ExtendedModels;
using TutorSearchSystem.Repositories.IRepositories;

namespace TutorSearchSystem.Repositories
{
    public class CourseDetailRepository : GenericRepository<CourseDetail>, ICourseDetailRepository
    {
        private readonly TSDbContext _context;
        public CourseDetailRepository(TSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseDetail>> GetByCourse(int courseId)
        {
            return await _context.CourseDetail.Where(c => c.CourseId == courseId).OrderBy(c => c.Period).ToListAsync();
        }
    }
}
