using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Filters;
using TutorSearchSystem.Global;
using TutorSearchSystem.Models;
using TutorSearchSystem.Models.ExtendedModels;
using TutorSearchSystem.Repositories.IRepositories;

namespace TutorSearchSystem.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly TSDbContext _context;

        public CourseRepository(TSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CheckCourseByClassHasSubject(int id)
        {
            string[] statuses = { GlobalConstants.ACTIVE_STATUS, GlobalConstants.ONGOING_STATUS, GlobalConstants.PENDING_STATUS, GlobalConstants.UNPAID_STATUS };
            return await _context.Course.Where(c => c.ClassHasSubjectId == id
            && statuses.Contains(c.Status)).CountAsync();
        }

        public async Task<int> CheckCourseByTutorId(int tutorId)
        {
            var result = await _context.Course.Where(c => c.CreatedBy == tutorId && (
               c.Status == GlobalConstants.ACTIVE_STATUS 
            || c.Status == GlobalConstants.PENDING_STATUS 
            || c.Status == GlobalConstants.UNPAID_STATUS
            || c.Status == GlobalConstants.ONGOING_STATUS)).ToListAsync();
            return result.Count;
        }

        //trung gio, trung ngay hoc, trung thu trong tuan, khong phai la denied course
        public async Task<Course> CheckValidate(CourseDto courseDto)
        {
            IEnumerable<Course> courses = await _context.Course
                .Where(c => c.BeginDate.Date.CompareTo(courseDto.BeginDate.Date) <= 0 
                && c.EndDate.Date.CompareTo(courseDto.BeginDate.Date) >= 0
                && c.BeginTime.TimeOfDay.CompareTo(courseDto.BeginTime.TimeOfDay) <= 0
                && c.EndTime.TimeOfDay.CompareTo(courseDto.BeginTime.TimeOfDay) >= 0
                && c.CreatedBy == courseDto.CreatedBy 
                && c.Status != GlobalConstants.DENIED_STATUS)
                .ToListAsync();
            //
            string[] tmpWeekdays = courseDto.DaysInWeek.Trim().Replace("[","").Replace("]","").Split(",");
            foreach (var c in courses)
            {
                foreach (var tmp in tmpWeekdays)
                {
                    if(c.DaysInWeek.Contains(tmp))
                    {
                        return c;
                    }
                }
            }
            //
            return null;
        }

        public async Task<PagedList<ExtendedCourse>> Filter(CourseParameter parameter)
        {
            if (parameter.ManagerId == 0)
            {
                var entities = await _context.Course.Where(
                c => c.Name.Contains(parameter.CourseName)
                && (c.CreatedDate.Date >= parameter.FromDate.Date && c.CreatedDate.Date <= parameter.ToDate.Date)
                ).Select(e => new ExtendedCourse
                {
                    Description = e.Description,
                    Status = e.Status,
                    Id = e.Id,
                    Name = e.Name,
                    BeginTime = e.BeginTime,
                    EndTime = e.EndTime,
                    StudyFee = e.StudyFee,
                    DaysInWeek = e.DaysInWeek,
                    BeginDate = e.BeginDate,
                    EndDate = e.EndDate,
                    ClassHasSubjectId = e.ClassHasSubjectId,
                    ConfirmedBy = e.ConfirmedBy,
                    ConfirmedDate = e.ConfirmedDate,
                    CreatedBy = e.CreatedBy,
                    CreatedDate = e.CreatedDate,
                    MaxTutee = e.MaxTutee,
                    Location = e.Location,
                    ExtraImages = e.ExtraImages,
                    SubjectName = e.ClassHasSubject.Subject.Name,
                    ClassName = e.ClassHasSubject.Class.Name,
                    TutorAvatarUrl = e.Tutor.AvatarImageLink,
                    TutorName = e.Tutor.Fullname,
                    TutorEmail = e.Tutor.Email,
                    ConfirmerName = e.Manager.Fullname,
                    NumberOfTutee = e.Enrollments.Count,
                    Precondition = e.Precondition
                })
                .Where(
                    c => c.SubjectName.Contains(parameter.SubjectName)
                    && c.TutorName.Contains(parameter.TutorName)).OrderByDescending(c => c.CreatedDate).ToListAsync();
                if (!String.IsNullOrEmpty(parameter.Status))
                {
                    entities = entities.Where(c => c.Status == parameter.Status).ToList();
                }
                return PagedList<ExtendedCourse>.ToPagedList(entities, parameter.PageNumber, parameter.PageSize);
            }
            else
            {
                var entities = await _context.Course.Where(
                c => c.Name.Contains(parameter.CourseName)
                && (c.CreatedDate.Date >= parameter.FromDate.Date && c.CreatedDate.Date <= parameter.ToDate.Date)
                && c.ClassHasSubject.Subject.ManageBy == parameter.ManagerId
                ).Select(e => new ExtendedCourse
                {
                    Description = e.Description,
                    Status = e.Status,
                    Id = e.Id,
                    Name = e.Name,
                    BeginTime = e.BeginTime,
                    EndTime = e.EndTime,
                    StudyFee = e.StudyFee,
                    DaysInWeek = e.DaysInWeek,
                    BeginDate = e.BeginDate,
                    EndDate = e.EndDate,
                    ClassHasSubjectId = e.ClassHasSubjectId,
                    ConfirmedBy = e.ConfirmedBy,
                    ConfirmedDate = e.ConfirmedDate,
                    CreatedBy = e.CreatedBy,
                    CreatedDate = e.CreatedDate,
                    MaxTutee = e.MaxTutee,
                    Location = e.Location,
                    ExtraImages = e.ExtraImages,
                    SubjectName = e.ClassHasSubject.Subject.Name,
                    ClassName = e.ClassHasSubject.Class.Name,
                    TutorAvatarUrl = e.Tutor.AvatarImageLink,
                    TutorName = e.Tutor.Fullname,
                    TutorEmail = e.Tutor.Email,
                    ConfirmerName = e.Manager.Fullname,
                    NumberOfTutee = e.Enrollments.Count,
                    Precondition = e.Precondition
                })
                .Where(
                    c => c.SubjectName.Contains(parameter.SubjectName)
                    && c.TutorName.Contains(parameter.TutorName)).OrderByDescending(c => c.CreatedDate).ToListAsync();
                if (!String.IsNullOrEmpty(parameter.Status))
                {
                    entities = entities.Where(c => c.Status == parameter.Status).ToList();
                }
                return PagedList<ExtendedCourse>.ToPagedList(entities, parameter.PageNumber, parameter.PageSize);
            }
        }

        //get coourses of a tutor by course status
        public async Task<IEnumerable<Course>> Get(int tutorId, string status)
        {
            if (status != "All")
            {
                return await _context.Course.Where(
                c => c.Tutor.Id == tutorId && c.Status == status).OrderByDescending(t => t.CreatedDate).ToListAsync();
            }
            else
            {
                return await _context.Course.Where(
                c => c.Tutor.Id == tutorId).OrderByDescending(t => t.CreatedDate).ToListAsync();
            }

        }

        //get all active course except courses following by tuteeId
        public async Task<IEnumerable<CourseTutor>> GetActiveUnregisteredCourses(int tuteeId)
        {
            return await _context.Course.Where(c => c.Status == GlobalConstants.ACTIVE_STATUS)
                //get all course except courses that register by this tutee
                .Except<Course>(_context.Course.Where(c => c.Enrollments.Any(e => e.TuteeId == tuteeId))
                ////if the number of follower in this course is less than maxtutee => get
                .Except(_context.Course.Where(c => c.Enrollments.Where(e => e.Status == GlobalConstants.ACTIVE_STATUS).Count() >= c.MaxTutee)
                )
                )
                .Join(
                _context.Tutor,
                c => c.CreatedBy,
                t => t.Id,
                (c, t) => new CourseTutor
                {
                    ExtraImages = c.ExtraImages,
                    Id = c.Id,
                    Location = c.Location,
                    Name = c.Name,
                    BeginTime = c.BeginTime,
                    EndTime = c.EndTime,
                    StudyFee = c.StudyFee,
                    DaysInWeek = c.DaysInWeek,
                    BeginDate = c.BeginDate,
                    EndDate = c.EndDate,
                    ClassHasSubjectId = c.ClassHasSubjectId,
                    ConfirmedBy = c.ConfirmedBy,
                    ConfirmedDate = c.ConfirmedDate,
                    CreatedBy = c.CreatedBy,
                    CreatedDate = c.CreatedDate,
                    MaxTutee = c.MaxTutee,
                    Description = c.Description,
                    Status = c.Status,
                    Fullname = t.Fullname,
                    Gender = t.Gender,
                    Birthday = t.Birthday,
                    Email = t.Email,
                    Phone = t.Phone,
                    AvatarImageLink = t.AvatarImageLink,
                    Address = t.Address,
                    RoleId = t.RoleId,
                    SubjectId = c.ClassHasSubject.SubjectId,
                    Precondition = c.Precondition
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ExtendedCourse>> GetAllExtended()
        {
            var entities = await _context.Course
                .Join(
                _context.Subject,
                c => c.ClassHasSubject.SubjectId,
                s => s.Id,
                (c, s) => new ExtendedCourse
                {
                    ExtraImages = c.ExtraImages,
                    Location = c.Location,
                    Id = c.Id,
                    Name = c.Name,
                    BeginTime = c.BeginTime,
                    EndTime = c.EndTime,
                    StudyFee = c.StudyFee,
                    DaysInWeek = c.DaysInWeek,
                    BeginDate = c.BeginDate,
                    EndDate = c.EndDate,
                    ClassHasSubjectId = c.ClassHasSubjectId,
                    ConfirmedBy = c.ConfirmedBy,
                    ConfirmedDate = c.ConfirmedDate,
                    CreatedBy = c.CreatedBy,
                    CreatedDate = c.CreatedDate,
                    MaxTutee = c.MaxTutee,
                    Description = c.Description,
                    Status = c.Status,
                    SubjectName = s.Name,
                    Precondition = c.Precondition
                }
                )
               .Join(
                   _context.Tutor,
                   c => c.CreatedBy,
                   t => t.Id,
                   (c, t) => new ExtendedCourse
                   {
                       ExtraImages = c.ExtraImages,
                       Location = c.Location,
                       Id = c.Id,
                       Name = c.Name,
                       BeginTime = c.BeginTime,
                       EndTime = c.EndTime,
                       StudyFee = c.StudyFee,
                       DaysInWeek = c.DaysInWeek,
                       BeginDate = c.BeginDate,
                       EndDate = c.EndDate,
                       ClassHasSubjectId = c.ClassHasSubjectId,
                       ConfirmedBy = c.ConfirmedBy,
                       ConfirmedDate = c.ConfirmedDate,
                       CreatedBy = c.CreatedBy,
                       CreatedDate = c.CreatedDate,
                       MaxTutee = c.MaxTutee,
                       Description = c.Description,
                       Status = c.Status,
                       SubjectName = c.SubjectName,
                       TutorName = t.Fullname,
                       TutorAvatarUrl = t.AvatarImageLink,
                       TutorEmail = t.Email,
                       Precondition = c.Precondition
                   }
               )
               .ToListAsync();

            return entities.Join(_context.Manager,
               c => c.ConfirmedBy,
               m => m.Id, (c, m) =>
               new ExtendedCourse
               {
                   ExtraImages = c.ExtraImages,
                   Location = c.Location,
                   Id = c.Id,
                   Name = c.Name,
                   BeginTime = c.BeginTime,
                   EndTime = c.EndTime,
                   StudyFee = c.StudyFee,
                   DaysInWeek = c.DaysInWeek,
                   BeginDate = c.BeginDate,
                   EndDate = c.EndDate,
                   ClassHasSubjectId = c.ClassHasSubjectId,
                   ConfirmedBy = c.ConfirmedBy,
                   ConfirmedDate = c.ConfirmedDate,
                   CreatedBy = c.CreatedBy,
                   CreatedDate = c.CreatedDate,
                   MaxTutee = c.MaxTutee,
                   Description = c.Description,
                   Status = c.Status,
                   TutorName = c.TutorName,
                   TutorAvatarUrl = c.TutorAvatarUrl,
                   SubjectName = c.SubjectName,
                   TutorEmail = c.TutorEmail,
                   ConfirmerName = m.Fullname,
                   Precondition = c.Precondition
               })
                .Union(entities.Where(
                    c => c.Status == GlobalConstants.PENDING_STATUS)
                );
        }

        public async Task<IEnumerable<ExtendedCourse>> GetCoursesByManagerId(int managerId)
        {
            //return await _context.Course.Where(
            //    c => c.ClassHasSubject.Subject.ManageBy == managerId).ToListAsync();
            var entities = await _context.Course
                .Where(
               c => c.ClassHasSubject.Subject.ManageBy == managerId)
                .Join(
                _context.Subject,
                c => c.ClassHasSubject.SubjectId,
                s => s.Id,
                (c, s) => new ExtendedCourse
                {
                    ExtraImages = c.ExtraImages,
                    Location = c.Location,
                    Id = c.Id,
                    Name = c.Name,
                    BeginTime = c.BeginTime,
                    EndTime = c.EndTime,
                    StudyFee = c.StudyFee,
                    DaysInWeek = c.DaysInWeek,
                    BeginDate = c.BeginDate,
                    EndDate = c.EndDate,
                    ClassHasSubjectId = c.ClassHasSubjectId,
                    ConfirmedBy = c.ConfirmedBy,
                    ConfirmedDate = c.ConfirmedDate,
                    CreatedBy = c.CreatedBy,
                    CreatedDate = c.CreatedDate,
                    MaxTutee = c.MaxTutee,
                    Description = c.Description,
                    Status = c.Status,
                    SubjectName = s.Name,
                    Precondition = c.Precondition
                }
                )
               .Join(
                   _context.Tutor,
                   c => c.CreatedBy,
                   t => t.Id,
                   (c, t) => new ExtendedCourse
                   {
                       ExtraImages = c.ExtraImages,
                       Location = c.Location,
                       Id = c.Id,
                       Name = c.Name,
                       BeginTime = c.BeginTime,
                       EndTime = c.EndTime,
                       StudyFee = c.StudyFee,
                       DaysInWeek = c.DaysInWeek,
                       BeginDate = c.BeginDate,
                       EndDate = c.EndDate,
                       ClassHasSubjectId = c.ClassHasSubjectId,
                       ConfirmedBy = c.ConfirmedBy,
                       ConfirmedDate = c.ConfirmedDate,
                       CreatedBy = c.CreatedBy,
                       CreatedDate = c.CreatedDate,
                       MaxTutee = c.MaxTutee,
                       Description = c.Description,
                       Status = c.Status,
                       SubjectName = c.SubjectName,
                       TutorName = t.Fullname,
                       TutorAvatarUrl = t.AvatarImageLink,
                       Precondition = c.Precondition
                   }
               )
               .ToListAsync();

            return entities.Join(_context.Manager,
               c => c.ConfirmedBy,
               m => m.Id, (c, m) =>
               new ExtendedCourse
               {
                   ExtraImages = c.ExtraImages,
                   Location = c.Location,
                   Id = c.Id,
                   Name = c.Name,
                   BeginTime = c.BeginTime,
                   EndTime = c.EndTime,
                   StudyFee = c.StudyFee,
                   DaysInWeek = c.DaysInWeek,
                   BeginDate = c.BeginDate,
                   EndDate = c.EndDate,
                   ClassHasSubjectId = c.ClassHasSubjectId,
                   ConfirmedBy = c.ConfirmedBy,
                   ConfirmedDate = c.ConfirmedDate,
                   CreatedBy = c.CreatedBy,
                   CreatedDate = c.CreatedDate,
                   MaxTutee = c.MaxTutee,
                   Description = c.Description,
                   Status = c.Status,
                   TutorName = c.TutorName,
                   TutorAvatarUrl = c.TutorAvatarUrl,
                   SubjectName = c.SubjectName,
                   ConfirmerName = m.Fullname,
                   Precondition = c.Precondition
               })
                .Union(entities.Where(
                    c => c.Status == GlobalConstants.PENDING_STATUS)
                );

        }

        //select course by tuteeId and Enrollment status: accepted, denied, pending
        public async Task<IEnumerable<ExtendedCourse>> Search(int tuteeId, string enrollmentStatus)
        {
            return await _context.Course.Where(
                c => c.Enrollments.Any(
                    e => e.Status == enrollmentStatus
                    && e.TuteeId == tuteeId))
                .Join(
                   _context.Tutor,
                   c => c.CreatedBy,
                   t => t.Id,
                   (c, t) => new ExtendedCourse
                   {
                       ExtraImages = c.ExtraImages,
                       Location = c.Location,
                       Id = c.Id,
                       Name = c.Name,
                       BeginTime = c.BeginTime,
                       EndTime = c.EndTime,
                       StudyFee = c.StudyFee,
                       DaysInWeek = c.DaysInWeek,
                       BeginDate = c.BeginDate,
                       EndDate = c.EndDate,
                       ClassHasSubjectId = c.ClassHasSubjectId,
                       ConfirmedBy = c.ConfirmedBy,
                       ConfirmedDate = c.ConfirmedDate,
                       CreatedBy = c.CreatedBy,
                       CreatedDate = c.CreatedDate,
                       MaxTutee = c.MaxTutee,
                       Description = c.Description,
                       Status = c.Status,
                       TutorAvatarUrl = t.AvatarImageLink,
                       TutorName = t.Fullname,
                       Precondition = c.Precondition
                   }
               )
                 .Join(
                   _context.Enrollment,
                   c => c.Id,
                   e => e.CourseId,
                   (c, e) => new ExtendedCourse
                   {
                       ExtraImages = c.ExtraImages,
                       Location = c.Location,
                       Id = c.Id,
                       Name = c.Name,
                       BeginTime = c.BeginTime,
                       EndTime = c.EndTime,
                       StudyFee = c.StudyFee,
                       DaysInWeek = c.DaysInWeek,
                       BeginDate = c.BeginDate,
                       EndDate = c.EndDate,
                       ClassHasSubjectId = c.ClassHasSubjectId,
                       ConfirmedBy = c.ConfirmedBy,
                       ConfirmedDate = c.ConfirmedDate,
                       CreatedBy = c.CreatedBy,
                       CreatedDate = c.CreatedDate,
                       MaxTutee = c.MaxTutee,
                       Description = c.Description,
                       Status = c.Status,
                       TutorAvatarUrl = c.TutorAvatarUrl,
                       TutorName = c.TutorName,
                       EnrollmentStatus = e.Status,
                       EnrollmentId = e.Id,
                       TuteeId = e.TuteeId,
                       FollowDate = e.CreatedDate,
                       Precondition = c.Precondition
                   }
               )
                 .Where(
                c => c.EnrollmentStatus == enrollmentStatus
                && c.TuteeId == tuteeId
                )
                 .OrderByDescending(c => c.FollowDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ExtendedCourse>> Search(int tuteeId)
        {
            return await _context.Enrollment.Where(e => e.TuteeId == tuteeId).Select(e => new ExtendedCourse
            {
                ExtraImages = e.Course.ExtraImages,
                Location = e.Course.Location,
                Id = e.Course.Id,
                Name = e.Course.Name,
                BeginTime = e.Course.BeginTime,
                EndTime = e.Course.EndTime,
                StudyFee = e.Course.StudyFee,
                DaysInWeek = e.Course.DaysInWeek,
                BeginDate = e.Course.BeginDate,
                EndDate = e.Course.EndDate,
                ClassHasSubjectId = e.Course.ClassHasSubjectId,
                ConfirmedBy = e.Course.ConfirmedBy,
                ConfirmedDate = e.Course.ConfirmedDate,
                CreatedBy = e.Course.CreatedBy,
                CreatedDate = e.Course.CreatedDate,
                MaxTutee = e.Course.MaxTutee,
                Description = e.Course.Description,
                Status = e.Course.Status,
                TutorAvatarUrl = e.Course.Tutor.AvatarImageLink,
                TutorName = e.Course.Tutor.Fullname,
                EnrollmentStatus = e.Status,
                FollowDate = e.CreatedDate,
                
            }).ToListAsync();
            
        }

        public async Task<IEnumerable<Course>> Search(int tuteeId, int subjectId, int classId)
        {
            return await _context.Course.Where(c => c.Status == GlobalConstants.ACTIVE_STATUS
            && c.ClassHasSubject.ClassId == classId
            && c.ClassHasSubject.SubjectId == subjectId)
                .Except<Course>(_context.Course.Where(c => c.Enrollments.Any(e => e.TuteeId == tuteeId)))
                .ToListAsync();
        }

        public async Task<IEnumerable<CourseTutor>> Search(FilterQueryDto filter)
        {
            return await _context.Course.Where(c =>
           c.Status == GlobalConstants.ACTIVE_STATUS
           && c.ClassHasSubject.ClassId == filter.ClassId
           && c.ClassHasSubject.SubjectId == filter.SubjectId
           && c.StudyFee <= filter.MaxFee
           && c.StudyFee >= filter.MinFee
           && c.BeginDate.Date.CompareTo(filter.BeginDate.Date) >= 0
           && c.EndDate.Date.CompareTo(filter.EndDate.Date) <= 0
           && c.Tutor.Gender.Contains(filter.TutorGender)
           && c.Tutor.EducationLevel.Contains(filter.EducationLevel)
           && c.BeginTime.TimeOfDay.CompareTo(filter.MinTime.TimeOfDay) >= 0
           && c.EndTime.TimeOfDay.CompareTo(filter.MaxTime.TimeOfDay) <= 0
           && c.DaysInWeek.Contains(filter.Weekdays)
            )
                 .Except<Course>(_context.Course.Where(c => c.Enrollments.Any(e => e.TuteeId == filter.TuteeId))
                ////if the number of follower in this course is less than maxtutee => get
                .Except(_context.Course.Where(c => c.Enrollments.Where(e => e.Status == GlobalConstants.ACTIVE_STATUS).Count() >= c.MaxTutee)
                )
                )
                .Join(
                   _context.Tutor,
                   c => c.CreatedBy,
                   t => t.Id,
                   (c, t) => new CourseTutor
                   {
                       ExtraImages = c.ExtraImages,
                       Location = c.Location,
                       Id = c.Id,
                       Name = c.Name,
                       BeginTime = c.BeginTime,
                       EndTime = c.EndTime,
                       StudyFee = c.StudyFee,
                       DaysInWeek = c.DaysInWeek,
                       BeginDate = c.BeginDate,
                       EndDate = c.EndDate,
                       ClassHasSubjectId = c.ClassHasSubjectId,
                       ConfirmedBy = c.ConfirmedBy,
                       ConfirmedDate = c.ConfirmedDate,
                       CreatedBy = c.CreatedBy,
                       CreatedDate = c.CreatedDate,
                       MaxTutee = c.MaxTutee,
                       Description = c.Description,
                       Status = c.Status,
                       AvatarImageLink = t.AvatarImageLink,
                       Fullname = t.Fullname,
                       Precondition = c.Precondition
                   }
                   )
                .ToListAsync();

        }

        // get course by tutorId and select the latest created with status pending
        public async Task<IEnumerable<CourseTutor>>GetByTutor(int tutorId)
        {
            return await _context.Course.Where(c => c.CreatedBy == tutorId && c.Status == GlobalConstants.PENDING_STATUS).Select(c => new CourseTutor
            {
                ExtraImages = c.ExtraImages,
                Location = c.Location,
                Id = c.Id,
                Name = c.Name,
                BeginTime = c.BeginTime,
                EndTime = c.EndTime,
                StudyFee = c.StudyFee,
                DaysInWeek = c.DaysInWeek,
                BeginDate = c.BeginDate,
                EndDate = c.EndDate,
                ClassHasSubjectId = c.ClassHasSubjectId,
                ConfirmedBy = c.ConfirmedBy,
                ConfirmedDate = c.ConfirmedDate,
                CreatedBy = c.CreatedBy,
                CreatedDate = c.CreatedDate,
                MaxTutee = c.MaxTutee,
                Description = c.Description,
                Status = c.Status,
                AvatarImageLink = c.Tutor.AvatarImageLink,
                Fullname = c.Tutor.Fullname,
                Precondition = c.Precondition
            }).OrderByDescending(c => c.CreatedDate).ToListAsync();
            
        }

        //public async Task<IEnumerable<Course>> Search(int subjectId, float minFee, float maxFee, String daysInWeek, DateTime beginDate, String minTime, String maxTime, String tutorGender)
        //{
        //    IQueryable<Course> query = _context.Course;

        //    query = query.Where(c => c.Status == GlobalConstants.ACTIVE_STATUS
        //    && c.ClassHasSubject.SubjectId == subjectId
        //    && c.StudyFee <= maxFee
        //    && c.StudyFee >= minFee
        //    && c.DaysInWeek.Contains(daysInWeek)
        //    && c.BeginDate.CompareTo(beginDate) > 0
        //    && c.Tutor.Gender == tutorGender
        //    );

        //    return await query.ToListAsync();
        //}
    }
}
