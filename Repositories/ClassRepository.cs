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
    public class ClassRepository : GenericRepository<Class>, IClassRepository
    {
        private readonly TSDbContext _context;

        public ClassRepository(TSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckExist(string name)
        {
            return await _context.Class.AnyAsync(c => c.Name == name);
        }

        public async Task<PagedList<Class>> Filter(ClassParameter parameter)
        {
            var entites = await _context.Class.Where(c =>
            c.Name.Contains(parameter.Name)
            ).OrderByDescending(t => t.CreatedDate).ToListAsync();
            if (!String.IsNullOrEmpty(parameter.Status))
            {
                entites = entites.Where(c => c.Status.ToUpper() == parameter.Status.ToUpper()).ToList();
            }
            return PagedList<Class>.ToPagedList(entites, parameter.PageNumber, parameter.PageSize);
        }

        public async Task<ExtendedClass> GetExtendedClassById(int id)
        {
            return await _context.Class.Where(c => c.Id == id)
                .Join(_context.Manager,
                 c => c.UpdatedBy,
                 m => m.Id,
                 (c, m) => new ExtendedClass
                 {
                     Id = c.Id,
                     Name = c.Name,
                     UpdatedBy = c.UpdatedBy,
                     UpdatedDate = c.UpdatedDate,
                     CreatedBy = c.CreatedBy,
                     CreatedDate = c.CreatedDate,
                     Status = c.Status,
                     Description = c.Description,
                     UpdaterName = m.Fullname,
                 })
                .Join(_context.Manager,
                 c => c.CreatedBy,
                 m => m.Id,
                 (c, m) => new ExtendedClass
                 {
                     Id = c.Id,
                     Name = c.Name,
                     UpdatedBy = c.UpdatedBy,
                     UpdatedDate = c.UpdatedDate,
                     CreatedBy = c.CreatedBy,
                     CreatedDate = c.CreatedDate,
                     Status = c.Status,
                     Description = c.Description,
                     UpdaterName = c.UpdaterName,
                     CreatorName = m.Fullname,
                 })
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Class>> GetActive()
        {
            return await _context.Class.Where(c => c.Status == GlobalConstants.ACTIVE_STATUS).Select(c => new Class
            {
                Id = c.Id,
                Name = c.Name
            }).OrderBy(o => o.Name).ToListAsync();
        }

        public async Task<IEnumerable<Class>> Search(int subjectId, string status)
        {
            return await _context.Class.Where(
                c => c.ClassHasSubjects.Any(x => x.SubjectId == subjectId)
                && c.Status == status).ToListAsync();
        }
    }
}
