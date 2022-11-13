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
    public class ManagerRepository : GenericRepository<Manager>, IManagerRepository
    {
        private readonly TSDbContext _context;
        public ManagerRepository(TSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedList<Manager>> Filter(ManagerParameter parameter)
        {
            var entities = await _context.Manager.Where(m => m.Fullname.Contains(parameter.ManagerName)
            && m.Email.Contains(parameter.Email)
            && m.Status.Contains(parameter.Status)
            && m.RoleId == 2).Select(m => new Manager {
                    Id = m.Id,
                    CreatedDate = m.CreatedDate,
                    CreatedBy = m.CreatedBy,
                    Fullname = m.Fullname,
                    Gender = m.Gender,
                    Birthday = m.Birthday,
                    Email = m.Email,
                    Phone = m.Phone,
                    AvatarImageLink = m.AvatarImageLink,
                    Address = m.Address,
                    RoleId = m.RoleId,
                    Description = m.Description,
                    Status = m.Status
                }).OrderByDescending(o => o.CreatedDate).ToListAsync();
            return PagedList<Manager>.ToPagedList(entities, parameter.PageNumber, parameter.PageSize);
        }

        public async Task<IEnumerable<Manager>> GetAllByStatus(string status)
        {
            return await _context.Manager.Where(m => m.Status == status).Select(m => new Manager
            {
                Id = m.Id,
                Fullname = m.Fullname,
                Email = m.Email
            }).OrderBy(o => o.Fullname).ToListAsync();
        }

     

        public async Task<Manager> GetByEmail(string email)
        {
            return await _context.Manager.Where(m =>
            m.Email == email
            && m.Status == GlobalConstants.ACTIVE_STATUS)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ExtendedManager>> GetByRoleId(int roleId)
        {
            return await _context.Manager
                .Where(m => m.RoleId == roleId)
                .Join(
                _context.Manager,
                m => m.CreatedBy,
                c => c.Id,
                (m, c) => new ExtendedManager
                {
                    CreatorName = c.Fullname,
                    Id = m.Id,
                    CreatedDate = m.CreatedDate,
                    CreatedBy = m.CreatedBy,
                    Fullname = m.Fullname,
                    Gender = m.Gender,
                    Birthday = m.Birthday,
                    Email = m.Email,
                    Phone = m.Phone,
                    AvatarImageLink = m.AvatarImageLink,
                    Address = m.Address,
                    RoleId = m.RoleId,
                    Description = m.Description,
                    Status = m.Status,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<Manager>> GetByStatus(string status, int role)
        {
            return await _context.Manager.Where(m => m.Status == status && m.RoleId == role).Select(m => new Manager
            {
                Id = m.Id,
                Fullname = m.Fullname,
                Email = m.Email
            }).OrderBy(o => o.Fullname).ToListAsync();
        }

        public async Task<string> GetManagerEmailByClassHasSubject(int id)
        {
            var entity = await _context.Class_Has_Subject.Where(c => c.Id == id).Select(c => new Manager
            {
                Id = c.Subject.SubjectManager.Id,
                Email = c.Subject.SubjectManager.Email

            }).FirstAsync();
            return entity.Email;
        }
    }
}
