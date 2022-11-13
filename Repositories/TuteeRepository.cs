using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Models;
using TutorSearchSystem.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using TutorSearchSystem.Global;
using TutorSearchSystem.Filters;

namespace TutorSearchSystem.Repositories
{
    public class TuteeRepository : GenericRepository<Tutee>, ITuteeRepository
    {
        private readonly TSDbContext _context;

        public TuteeRepository(TSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedList<Tutee>> Filter(TuteeParameter parameter)
        {
            var entities = await _context.Tutee.Where(t =>
            t.Fullname.Contains(parameter.TuteeName)
            && t.Email.Contains(parameter.Email)).OrderByDescending(t => t.CreatedDate).ToListAsync();
            return PagedList<Tutee>.ToPagedList(entities, parameter.PageNumber, parameter.PageSize);
        }

        public async Task<Tutee> Get(string email)
        {
            return await _context.Tutee.Where(t => t.Email == email).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountInMonth()
        {
            DateTime fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var tutors = await _context.Tutee.Where(t => t.Status == GlobalConstants.ACTIVE_STATUS && t.CreatedDate >= fromDate).ToListAsync();
            return tutors.Count;
        }

        //get tutees in a course
        public async Task<IEnumerable<Tutee>> GetTuteeInCourse(int courseId)
        {
            return await _context.Tutee.Where(
                t => t.Enrollements.Any(e => e.CourseId == courseId))
                .ToListAsync();
        }
    }
}
