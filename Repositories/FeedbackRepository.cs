using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Global;
using TutorSearchSystem.Models;
using TutorSearchSystem.Models.ExtendedModels;
using TutorSearchSystem.Repositories.IRepositories;

namespace TutorSearchSystem.Repositories
{
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
        private readonly TSDbContext _context;
        public FeedbackRepository(TSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Feedback.FindAsync(id);
            if(entity == null)
            {
                return false;
            }
            try
            {
                _context.Feedback.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public async Task<IEnumerable<ExtendedFeedback>> GetAllExtended()
        {
            return await _context.Feedback
                .Where(f => f.Status == GlobalConstants.PENDING_STATUS)
                .Select(f =>
                new ExtendedFeedback
                {
                    Id = f.Id,
                    TuteeId = f.TuteeId,
                    TutorId = f.TutorId,
                    Comment = f.Comment,
                    Rate = f.Rate,
                    CreatedDate = f.CreatedDate,
                    ConfirmedBy = f.ConfirmedBy,
                    ConfirmedDate = f.ConfirmedDate,
                    Status = f.Status,
                    TutorName = f.Tutor.Fullname,
                    TuteeName = f.Tutee.Fullname,
                    CourseName = f.Course.Name,
                    SubjectName = f.Course.ClassHasSubject.Subject.Name,
                    ClassName = f.Course.ClassHasSubject.Class.Name
                }
               ).OrderByDescending(f => f.CreatedDate)
               .ToListAsync();
        }

        public async Task<double> GetAverageRatingStateByTutorId(int tutorId)
        {
            var counter = await _context.Feedback.Where(f => f.TutorId == tutorId).CountAsync();
            if( counter > 0)
            {
                return await _context.Feedback.Where(f => f.TutorId == tutorId).AverageAsync(f => f.Rate);
            }
            return 0;
        }

        public async Task<IEnumerable<ExtendedFeedback>> GetByManagerId(int managerId)
        {
            return await _context.Feedback.Where(
               f => f.Course.ClassHasSubject.Subject.ManageBy == managerId
               && f.Status == GlobalConstants.PENDING_STATUS).Select(f => 
                new ExtendedFeedback
                   {
                       Id = f.Id,
                       TuteeId = f.TuteeId,
                       TutorId = f.TutorId,
                       Comment = f.Comment,
                       Rate = f.Rate,
                       CreatedDate = f.CreatedDate,
                       ConfirmedBy = f.ConfirmedBy,
                       ConfirmedDate = f.ConfirmedDate,
                       Status = f.Status,
                       TutorName = f.Tutor.Fullname,
                       TuteeName = f.Tutee.Fullname,
                       CourseName = f.Course.Name,
                       SubjectName = f.Course.ClassHasSubject.Subject.Name,
                       ClassName = f.Course.ClassHasSubject.Class.Name
                   }
               )
               .ToListAsync();
        }

        public async Task<IEnumerable<ExtendedFeedback>> GetByTutorId(int tutorId)
        {
            return await _context.Feedback.Where(f => f.TutorId == tutorId && f.Status == GlobalConstants.ACTIVE_STATUS)
                .Join(
                   _context.Tutee,
                   f => f.TuteeId,
                   t => t.Id,
                   (f, t) => new ExtendedFeedback
                   {
                       Id = f.Id,
                       TuteeId = f.TuteeId,
                       TutorId = f.TutorId,
                       Comment = f.Comment,
                       Rate = f.Rate,
                       CreatedDate = f.CreatedDate,
                       ConfirmedBy = f.ConfirmedBy,
                       ConfirmedDate = f.ConfirmedDate,
                       Status = f.Status,
                       TuteeName = t.Fullname,
                       TuteeAvatarUrl = t.AvatarImageLink,
                       CourseName = f.Course.Name,
                       SubjectName = f.Course.ClassHasSubject.Subject.Name,
                       ClassName = f.Course.ClassHasSubject.Class.Name
                   }
               )
                .ToListAsync();
        }

        //get first Tutor: in enrollment of tutee but does not feedback yet
        public async Task<Tutor> Search(int tuteeId)
        {
            return await _context.Tutor.Where(
            t => t.Courses.Any(
                c => c.Enrollments.Any(
                    e => e.TuteeId == tuteeId && e.Status == GlobalConstants.ACCEPTED_STATUS) && c.EndDate.Date.CompareTo(Tools.GetUTC().Date) <= 0)).Except(_context.Tutor.Where(
                        t => t.Feedbacks.Any(
                            f => f.TuteeId == tuteeId))).FirstAsync();
        }
    }
}
