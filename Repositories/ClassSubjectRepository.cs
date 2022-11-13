using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Models;
using TutorSearchSystem.Global;
using TutorSearchSystem.Models.ExtendedModels;
using TutorSearchSystem.Repositories.IRepositories;
using TutorSearchSystem.Filters;

namespace TutorSearchSystem.Repositories
{
    public class ClassSubjectRepository : GenericRepository<ClassHasSubject>, IClassSubjectRepository
    {
        private readonly TSDbContext _context;

        public ClassSubjectRepository(TSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CheckByClass(int id)
        {
            return await _context.Class_Has_Subject.Where(c => c.ClassId == id && c.Status == GlobalConstants.ACTIVE_STATUS).CountAsync();
        }

        public async Task<int> CheckBySubject(int id)
        {
            return await _context.Class_Has_Subject.Where(c => c.SubjectId == id && c.Status == GlobalConstants.ACTIVE_STATUS).CountAsync();
        }

        public async Task<PagedList<ExtendedClassHasSubject>> Filter(ClassHasSubjectParameter parameter)
        {
            var entities = await _context.Class_Has_Subject.Where(c => c.Class.Name.Contains(parameter.ClassName)
            && c.Subject.Name.Contains(parameter.SubjectName)
            && c.Status.Contains(parameter.Status)).Select(c => new ExtendedClassHasSubject { 
                Id = c.Id,
                ClassName = c.Class.Name,
                SubjectName = c.Subject.Name,
                Status = c.Status,
                CreatedDate = c.CreatedDate
            }).OrderByDescending(o => o.CreatedDate).ToListAsync();
            return PagedList<ExtendedClassHasSubject>.ToPagedList(entities, parameter.PageNumber, parameter.PageSize);
        }

        public async Task<IEnumerable<ExtendedClassHasSubject>> GetAllExteded()
        {
            return await _context.Class_Has_Subject
                .Join(_context.Manager,
                h => h.Subject.ManageBy,
                m => m.Id,
                (h, m) => new
                {
                    h.Id,
                    h.ClassId,
                    h.SubjectId,
                    h.CreatedBy,
                    h.CreatedDate,
                    h.Description,
                    h.Status,
                    ManagerName = m.Fullname,
                })
                .Join(_context.Subject,
                h => h.SubjectId,
                s => s.Id,
                (h, s) => new
                {
                    h.Id,
                    h.ClassId,
                    h.SubjectId,
                    h.CreatedBy,
                    h.CreatedDate,
                    h.Description,
                    h.Status,
                    h.ManagerName,
                    SubjectName = s.Name,
                })
                .Join(_context.Class,
                h => h.ClassId,
                c => c.Id,
                (h, c) => new ExtendedClassHasSubject
                {
                    Id = h.Id,
                    ClassId = h.ClassId,
                    SubjectId = h.SubjectId,
                    CreatedBy = h.CreatedBy,
                    CreatedDate = h.CreatedDate,
                    Description = h.Description,
                    Status = h.Status,
                    SubjectName = h.SubjectName,
                    ManagerName = h.ManagerName,
                    ClassName = c.Name,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ExtendedClassHasSubject>> GetBySubject(int subjectId)
        {
            return await _context.Class_Has_Subject
                .Where( h => h.SubjectId == subjectId)
                .Join(_context.Manager,
                h => h.Subject.ManageBy,
                m => m.Id,
                (h, m) => new
                {
                    h.Id,
                    h.ClassId,
                    h.SubjectId,
                    h.CreatedBy,
                    h.CreatedDate,
                    h.Description,
                    h.Status,
                    ManagerName = m.Fullname,
                })
                .Join(_context.Subject,
                h => h.SubjectId,
                s => s.Id,
                (h, s) => new
                {
                    h.Id,
                    h.ClassId,
                    h.SubjectId,
                    h.CreatedBy,
                    h.CreatedDate,
                    h.Description,
                    h.Status,
                    h.ManagerName,
                    SubjectName = s.Name,
                })
                .Join(_context.Class,
                h => h.ClassId,
                c => c.Id,
                (h, c) => new ExtendedClassHasSubject
                {
                    Id = h.Id,
                    ClassId = h.ClassId,
                    SubjectId = h.SubjectId,
                    CreatedBy = h.CreatedBy,
                    CreatedDate = h.CreatedDate,
                    Description = h.Description,
                    Status = h.Status,
                    SubjectName = h.SubjectName,
                    ManagerName = h.ManagerName,
                    ClassName = c.Name,
                })
                .ToListAsync();
        }

        public async Task<ClassHasSubject> Search(int subjectId, int classId)
        {
            return await _context.Class_Has_Subject.Where(
                c => c.SubjectId == subjectId && c.ClassId == classId).FirstOrDefaultAsync();
        }
    }
}
