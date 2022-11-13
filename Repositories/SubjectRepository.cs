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
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        private readonly TSDbContext _context;

        public SubjectRepository(TSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckExist(string name)
        {
            return await _context.Subject.AnyAsync(s => s.Name == name);
        }

        public async Task<PagedList<ExtendedSubject>> Filter(SubjectParameter parameter)
        {
            var entities = await _context.Subject.Where(s => s.Name.Contains(parameter.SubjectName)
            && s.SubjectManager.Fullname.Contains(parameter.ManagerName)).Select(s => new ExtendedSubject { 
            Id = s.Id,
            Name = s.Name,
            CreatedDate= s.CreatedDate,
            ManagerName = s.SubjectManager.Fullname,
            Status = s.Status
            
            }).OrderByDescending(o => o.CreatedDate).ToListAsync();
            if (!String.IsNullOrEmpty(parameter.Status))
            {
                entities = entities.Where(s => s.Status.ToUpper() == parameter.Status.ToUpper()).ToList();
            }
            return PagedList<ExtendedSubject>.ToPagedList(entities, parameter.PageNumber, parameter.PageSize);
        }

        public async Task<IEnumerable<ExtendedSubject>> GetAllExtendedSubject()
        {
            return await _context.Subject
                .Join(_context.Manager,
                s => s.ManageBy,
                m => m.Id,
                (s, m) => new ExtendedSubject
                {
                    Id = s.Id,
                    Name = s.Name,
                    UpdatedBy = s.UpdatedBy,
                    UpdatedDate = s.UpdatedDate,
                    CreatedBy = s.CreatedBy,
                    CreatedDate = s.CreatedDate,
                    ManageBy = s.ManageBy,
                    Description = s.Description,
                    Status = s.Status,
                    ManagerName = m.Fullname,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<Subject>> GetByManagerId(int managerId)
        {
            return await _context.Subject.Where(s => s.ManageBy == managerId).ToListAsync();
        }

        public async Task<IEnumerable<Subject>> GetByStatus(string status)
        {
            return await _context.Subject.Where(s => s.Status == status && s.ClassHasSubjects.Count() > 0)
                .ToListAsync();
        }

        public async Task<IEnumerable<Subject>> GetByTutor(int tutorId)
        {
            return await _context.Subject.Where( 
                s => s.ClassHasSubjects
                .Any( h => h.Courses
                .Any( c => c.CreatedBy == tutorId)))
                .ToListAsync();
        }

        public async Task<ExtendedSubject> GetExtendedSubjectById(int id)
        {
            return await _context.Subject
                .Where(s => s.Id == id)
                .Join(_context.Manager,
                s => s.CreatedBy,
                m => m.Id,
                (s, m) => new ExtendedSubject
                {
                    Id = s.Id,
                    Name = s.Name,
                    UpdatedBy = s.UpdatedBy,
                    UpdatedDate = s.UpdatedDate,
                    CreatedBy = s.CreatedBy,
                    CreatedDate = s.CreatedDate,
                    ManageBy = s.ManageBy,
                    Description = s.Description,
                    Status = s.Status,
                    CreatorName = m.Fullname,
                })
                .Join(_context.Manager,
                s => s.UpdatedBy,
                m => m.Id,
                (s, m) => new ExtendedSubject
                {
                    Id = s.Id,
                    Name = s.Name,
                    UpdatedBy = s.UpdatedBy,
                    UpdatedDate = s.UpdatedDate,
                    CreatedBy = s.CreatedBy,
                    CreatedDate = s.CreatedDate,
                    ManageBy = s.ManageBy,
                    Description = s.Description,
                    Status = s.Status,
                    CreatorName = s.CreatorName,
                    UpdaterName = m.Fullname,
                })
                 .Join(_context.Manager,
                s => s.ManageBy,
                m => m.Id,
                (s, m) => new ExtendedSubject
                {
                    Id = s.Id,
                    Name = s.Name,
                    UpdatedBy = s.UpdatedBy,
                    UpdatedDate = s.UpdatedDate,
                    CreatedBy = s.CreatedBy,
                    CreatedDate = s.CreatedDate,
                    ManageBy = s.ManageBy,
                    Description = s.Description,
                    Status = s.Status,
                    CreatorName = s.CreatorName,
                    UpdaterName = s.UpdaterName,
                    ManagerName = m.Fullname,
                })
                .FirstOrDefaultAsync();
        }
    }
}
