using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Models;
using TutorSearchSystem.Models.ExtendedModels;
using TutorSearchSystem.Repositories.IRepositories;
using TutorSearchSystem.Global;

namespace TutorSearchSystem.Repositories
{
    public class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollmentRepository
    {
        private readonly TSDbContext _context;

        public EnrollmentRepository(TSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountEnrollmentByCourseId(int courseId)
        {
            return await _context.Enrollment.Where(e => e.CourseId == courseId).CountAsync();
        }

        public async Task<int> CountEnrollmentByCourseIdWithStatus(string status, int courseId)
        {
            return await _context.Enrollment.Where(e => e.CourseId == courseId && e.Status == status).CountAsync();
        }

        public async Task<IEnumerable<Enrollment>> EnrollmentByCourseId(int courseId)
        {
            return await _context.Enrollment.Where(e => e.CourseId == courseId).ToListAsync();
        }

        public async Task<Enrollment> Get(int courseId, int tuteeId)
        {
            return await _context.Enrollment.Where(
                e => e.CourseId == courseId
                && e.TuteeId == tuteeId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ExtendedEnrollment>> GetEnrollmentsByTutorId(int tutorId, DateTime toDate)
        {
            return await _context.Enrollment
                .Where(e => e.CreatedDate.Month == toDate.Month
               && e.CreatedDate.Year == toDate.Year
               && e.Course.CreatedBy == tutorId)
                .Join(
                _context.Course,
                e => e.CourseId,
                c => c.Id,
                (e, c) => new ExtendedEnrollment
                {
                    Id = e.Id,
                    TuteeId = e.TuteeId,
                    CourseId = e.CourseId,
                    CreatedDate = e.CreatedDate,
                    Description = e.Description,
                    Status = e.Status,
                    CourseName = c.Name,
                    StudyFee = c.StudyFee,
                })
                .Join(
                _context.Tutee,
                e => e.TuteeId,
                t => t.Id,
                (e, t) => new ExtendedEnrollment
                {
                    Id = e.Id,
                    TuteeId = e.TuteeId,
                    CourseId = e.CourseId,
                    CreatedDate = e.CreatedDate,
                    Description = e.Description,
                    Status = e.Status,
                    CourseName = e.CourseName,
                    StudyFee = e.StudyFee,
                    TuteeName = t.Fullname,
                })
                .ToListAsync();
        }
    }
}
