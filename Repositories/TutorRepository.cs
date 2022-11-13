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
    public class TutorRepository : GenericRepository<Tutor>, ITutorRepository
    {
        private readonly TSDbContext _context;

        public TutorRepository(TSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Tutor> Get(string email)
        {
            return await _context.Tutor.Where(t => t.Email == email).FirstOrDefaultAsync();
        }



        public async Task<IEnumerable<ExtendedTutor>> GetForManager(int managerId)
        {
            //contains all status that select for manager
            string[] statuses = { GlobalConstants.ACTIVE_STATUS, GlobalConstants.INACTIVE_STATUS };

            var entities = await _context.Tutor.Where(
               t =>
               t.Courses.Any(
                   c => c.ClassHasSubject.Subject.ManageBy == managerId)
               && statuses.Contains(t.Status)
               )
               .Join(_context.Membership,
               t => t.MembershipId,
               m => m.Id,
               (t, m) => new ExtendedTutor
               {
                   Fullname = t.Fullname,
                   Gender = t.Gender,
                   Birthday = t.Birthday,
                   Email = t.Email,
                   Phone = t.Phone,
                   AvatarImageLink = t.AvatarImageLink,
                   Address = t.Address,
                   RoleId = t.RoleId,
                   Id = t.Id,
                   EducationLevel = t.EducationLevel,
                   School = t.School,
                   Points = t.Points,
                   MembershipId = t.MembershipId,
                   SocialIdUrl = t.SocialIdUrl,
                   Description = t.Description,
                   Status = t.Status,
                   ConfirmedBy = t.ConfirmedBy,
                   ConfirmedDate = t.ConfirmedDate,
                   CreatedDate = t.CreatedDate,
                   MembershipName = m.Name,
               }).ToListAsync();

            return entities.Join(
             _context.Manager,
             t => t.ConfirmedBy,
             m => m.Id,
             (t, m) => new ExtendedTutor
             {
                 Fullname = t.Fullname,
                 Gender = t.Gender,
                 Birthday = t.Birthday,
                 Email = t.Email,
                 Phone = t.Phone,
                 AvatarImageLink = t.AvatarImageLink,
                 Address = t.Address,
                 RoleId = t.RoleId,
                 Id = t.Id,
                 EducationLevel = t.EducationLevel,
                 School = t.School,
                 Points = t.Points,
                 MembershipId = t.MembershipId,
                 SocialIdUrl = t.SocialIdUrl,
                 Description = t.Description,
                 Status = t.Status,
                 ConfirmedBy = t.ConfirmedBy,
                 ConfirmedDate = t.ConfirmedDate,
                 CreatedDate = t.CreatedDate,
                 MembershipName = t.MembershipName,
                 ConfirmerName = m.Fullname,
             })
             .Union(entities.Where(t => t.ConfirmedBy == null));

        }

        public async Task<IEnumerable<ExtendedTutor>> GetByStatus(string status)
        {
            return await _context.Tutor
                .Where(t => t.Status == status)
                .Join(_context.Membership,
               t => t.MembershipId,
               m => m.Id,
               (t, m) => new ExtendedTutor
               {
                   Fullname = t.Fullname,
                   Gender = t.Gender,
                   Birthday = t.Birthday,
                   Email = t.Email,
                   Phone = t.Phone,
                   AvatarImageLink = t.AvatarImageLink,
                   Address = t.Address,
                   RoleId = t.RoleId,
                   Id = t.Id,
                   EducationLevel = t.EducationLevel,
                   School = t.School,
                   Points = t.Points,
                   MembershipId = t.MembershipId,
                   SocialIdUrl = t.SocialIdUrl,
                   Description = t.Description,
                   Status = t.Status,
                   CreatedDate = t.CreatedDate,
                   MembershipName = m.Name,
               })
                .ToListAsync();
        }

        public async Task<IEnumerable<ExtendedTutor>> GetAllExtended()
        {
            //contains all status that select for manager
            string[] statuses = { GlobalConstants.ACTIVE_STATUS, GlobalConstants.INACTIVE_STATUS };

            var entities = await _context.Tutor.Where(
               t => statuses.Contains(t.Status)
               )
               .Join(_context.Membership,
               t => t.MembershipId,
               m => m.Id,
               (t, m) => new ExtendedTutor
               {
                   Fullname = t.Fullname,
                   Gender = t.Gender,
                   Birthday = t.Birthday,
                   Email = t.Email,
                   Phone = t.Phone,
                   AvatarImageLink = t.AvatarImageLink,
                   Address = t.Address,
                   RoleId = t.RoleId,
                   Id = t.Id,
                   EducationLevel = t.EducationLevel,
                   School = t.School,
                   Points = t.Points,
                   MembershipId = t.MembershipId,
                   SocialIdUrl = t.SocialIdUrl,
                   Description = t.Description,
                   Status = t.Status,
                   ConfirmedBy = t.ConfirmedBy,
                   ConfirmedDate = t.ConfirmedDate,
                   CreatedDate = t.CreatedDate,
                   MembershipName = m.Name,
               }).ToListAsync();

            return entities.Join(
             _context.Manager,
             t => t.ConfirmedBy,
             m => m.Id,
             (t, m) => new ExtendedTutor
             {
                 Fullname = t.Fullname,
                 Gender = t.Gender,
                 Birthday = t.Birthday,
                 Email = t.Email,
                 Phone = t.Phone,
                 AvatarImageLink = t.AvatarImageLink,
                 Address = t.Address,
                 RoleId = t.RoleId,
                 Id = t.Id,
                 EducationLevel = t.EducationLevel,
                 School = t.School,
                 Points = t.Points,
                 MembershipId = t.MembershipId,
                 SocialIdUrl = t.SocialIdUrl,
                 Description = t.Description,
                 Status = t.Status,
                 ConfirmedBy = t.ConfirmedBy,
                 ConfirmedDate = t.ConfirmedDate,
                 CreatedDate = t.CreatedDate,
                 MembershipName = t.MembershipName,
                 ConfirmerName = m.Fullname,
             })
             ;
        }

        public async Task<ExtendedTutor> GetExtendedById(int id)
        {
            var tutor = await _context.Tutor
               .FindAsync(id);

            return new ExtendedTutor
            {
                Fullname = tutor.Fullname,
                Gender = tutor.Gender,
                Birthday = tutor.Birthday,
                Email = tutor.Email,
                Phone = tutor.Phone,
                AvatarImageLink = tutor.AvatarImageLink,
                Address = tutor.Address,
                RoleId = tutor.RoleId,
                Id = tutor.Id,
                EducationLevel = tutor.EducationLevel,
                School = tutor.School,
                Points = tutor.Points,
                MembershipId = tutor.MembershipId,
                SocialIdUrl = tutor.SocialIdUrl,
                Description = tutor.Description,
                Status = tutor.Status,
                ConfirmedBy = tutor.ConfirmedBy,
                ConfirmedDate = tutor.ConfirmedDate,
                CreatedDate = tutor.CreatedDate,
                CertificationUrls = { },
            };


        }

        public async Task<int> GetCountInMonth()
        {
            DateTime fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var tutors =  await _context.Tutor.Where(t => t.Status == GlobalConstants.ACTIVE_STATUS && t.CreatedDate >= fromDate).ToListAsync();
            return tutors.Count;
        }

        public async Task<int> GetNumberOfCourseByTutorId(int tutorId)
        {
            return await _context.Course.Where(c => c.CreatedBy == tutorId 
            && c.Status != GlobalConstants.PENDING_STATUS 
            && c.Status != GlobalConstants.DENIED_STATUS)
                .CountAsync();
        }

        public async Task<int> GetNumberOfTuteeByTutorId(int tutorId)
        {
            return await _context.Tutee.Where(t => t.Enrollements.Any(e => e.Course.CreatedBy == tutorId))
                .GroupBy(t => t.Id).CountAsync();
        }

        public async Task<int> GetNumberOfFeedbackByTutorId(int tutorId)
        {
            return await _context.Feedback.Where(f => f.TutorId == tutorId
            && f.Status == GlobalConstants.ACTIVE_STATUS).CountAsync();
        }

        public async Task<PagedList<ExtendedTutor>> Filter(TutorParameter parameter)
        {
            string[] statuses = { GlobalConstants.ACTIVE_STATUS, GlobalConstants.INACTIVE_STATUS, GlobalConstants.DENIED_STATUS };
                if(parameter.ManagerId <= 0)
            {
                var entities = await _context.Tutor.Where(t =>
            statuses.Contains(t.Status)
            && t.Fullname.Contains(parameter.TutorName)
            && t.Email.Contains(parameter.Email)
            ).Join(
                _context.Membership,
               t => t.MembershipId,
               m => m.Id,
               (t, m) => new ExtendedTutor
               {
                   Fullname = t.Fullname,
                   Gender = t.Gender,
                   Birthday = t.Birthday,
                   Email = t.Email,
                   Phone = t.Phone,
                   AvatarImageLink = t.AvatarImageLink,
                   Address = t.Address,
                   RoleId = t.RoleId,
                   Id = t.Id,
                   EducationLevel = t.EducationLevel,
                   School = t.School,
                   Points = t.Points,
                   MembershipId = t.MembershipId,
                   SocialIdUrl = t.SocialIdUrl,
                   Description = t.Description,
                   Status = t.Status,
                   ConfirmedBy = t.ConfirmedBy,
                   ConfirmedDate = t.ConfirmedDate,
                   CreatedDate = t.CreatedDate,
                   MembershipName = m.Name,
               }
               ).Join(
                _context.Manager,
                t => t.ConfirmedBy,
                m => m.Id,
                (t, m) => new ExtendedTutor
                {
                    Fullname = t.Fullname,
                    Gender = t.Gender,
                    Birthday = t.Birthday,
                    Email = t.Email,
                    Phone = t.Phone,
                    AvatarImageLink = t.AvatarImageLink,
                    Address = t.Address,
                    RoleId = t.RoleId,
                    Id = t.Id,
                    EducationLevel = t.EducationLevel,
                    School = t.School,
                    Points = t.Points,
                    MembershipId = t.MembershipId,
                    SocialIdUrl = t.SocialIdUrl,
                    Description = t.Description,
                    Status = t.Status,
                    ConfirmedBy = t.ConfirmedBy,
                    ConfirmedDate = t.ConfirmedDate,
                    CreatedDate = t.CreatedDate,
                    MembershipName = t.MembershipName,
                    ConfirmerName = m.Fullname,
                    CertificationUrls = t.CertificationUrls
                }
                ).Where(t => t.Status.Contains(parameter.Status))
               .OrderByDescending(t => t.CreatedDate).ToListAsync();
                return PagedList<ExtendedTutor>.ToPagedList(entities, parameter.PageNumber, parameter.PageSize);
            }
            else
            {
                var entities = await _context.Tutor.Where(t =>
            statuses.Contains(t.Status)
            && t.Courses.Any(c => c.ClassHasSubject.Subject.ManageBy == parameter.ManagerId)
            && t.Fullname.Contains(parameter.TutorName)
            && t.Email.Contains(parameter.Email)
            ).Join(
                _context.Membership,
               t => t.MembershipId,
               m => m.Id,
               (t, m) => new ExtendedTutor
               {
                   Fullname = t.Fullname,
                   Gender = t.Gender,
                   Birthday = t.Birthday,
                   Email = t.Email,
                   Phone = t.Phone,
                   AvatarImageLink = t.AvatarImageLink,
                   Address = t.Address,
                   RoleId = t.RoleId,
                   Id = t.Id,
                   EducationLevel = t.EducationLevel,
                   School = t.School,
                   Points = t.Points,
                   MembershipId = t.MembershipId,
                   SocialIdUrl = t.SocialIdUrl,
                   Description = t.Description,
                   Status = t.Status,
                   ConfirmedBy = t.ConfirmedBy,
                   ConfirmedDate = t.ConfirmedDate,
                   CreatedDate = t.CreatedDate,
                   MembershipName = m.Name,
               }
               ).Join(
                _context.Manager,
                t => t.ConfirmedBy,
                m => m.Id,
                (t, m) => new ExtendedTutor
                {
                    Fullname = t.Fullname,
                    Gender = t.Gender,
                    Birthday = t.Birthday,
                    Email = t.Email,
                    Phone = t.Phone,
                    AvatarImageLink = t.AvatarImageLink,
                    Address = t.Address,
                    RoleId = t.RoleId,
                    Id = t.Id,
                    EducationLevel = t.EducationLevel,
                    School = t.School,
                    Points = t.Points,
                    MembershipId = t.MembershipId,
                    SocialIdUrl = t.SocialIdUrl,
                    Description = t.Description,
                    Status = t.Status,
                    ConfirmedBy = t.ConfirmedBy,
                    ConfirmedDate = t.ConfirmedDate,
                    CreatedDate = t.CreatedDate,
                    MembershipName = t.MembershipName,
                    ConfirmerName = m.Fullname,
                    CertificationUrls = t.CertificationUrls
                }
                ).Where(t => t.Status.Contains(parameter.Status))
               .OrderByDescending(t => t.CreatedDate).ToListAsync();
                return PagedList<ExtendedTutor>.ToPagedList(entities, parameter.PageNumber, parameter.PageSize);
            }
        }
    }
}
