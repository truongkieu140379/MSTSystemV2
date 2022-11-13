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
    public class MembershipRepository : GenericRepository<Membership>, IMembershipRepository
    {
        private readonly TSDbContext _context;
        public MembershipRepository(TSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckExist(string name)
        {
            return await _context.Membership.AnyAsync(m => m.Name == name);
        }

        public async Task<IEnumerable<ExtendedMembership>> GetAllExtendedMembership()
        {
            return await _context.Membership.Join(
               _context.Manager,
                  f => f.CreatedBy,
                  m => m.Id,
                  (f, m) => new
                  {
                      f.Id,
                      f.Name,
                      f.PointRate,
                      f.PointAmount,
                      f.UpdatedBy,
                      f.UpdatedDate,
                      f.CreatedBy,
                      f.CreatedDate,
                      f.Description,
                      f.Status,
                      CreatorFullname = m.Fullname,
                  })
               .Join(
                   _context.Manager,
                   f => f.UpdatedBy,
                  m => m.Id,
                  (f, m) => new ExtendedMembership
                  {
                      Id = f.Id,
                      Name = f.Name,
                      PointRate = f.PointRate,
                      PointAmount = f.PointAmount,
                      UpdatedBy = f.UpdatedBy,
                      UpdatedDate = f.UpdatedDate,
                      CreatedBy = f.CreatedBy,
                      CreatedDate = f.CreatedDate,
                      Description = f.Description,
                      Status = f.Status,
                      CreatorFullname = f.CreatorFullname,
                      UpdaterFullname = m.Fullname,
                  }
               )
               .Where(f => f.Status == GlobalConstants.ACTIVE_STATUS)
               .ToListAsync();
        }

        public async Task<IEnumerable<Membership>> GetByStatus(string status)
        {
            return await _context.Membership.Where(m => m.Status == status).OrderBy(m => m.PointRate).ToListAsync();
        }

        public async Task<ExtendedMembership> GetExtendedById(int id)
        {
            return await _context.Membership.Where(m => m.Id == id)
                .Join(
               _context.Manager,
                  f => f.CreatedBy,
                  m => m.Id,
                  (f, m) => new
                  {
                      f.Id,
                      f.Name,
                      f.PointRate,
                      f.PointAmount,
                      f.UpdatedBy,
                      f.UpdatedDate,
                      f.CreatedBy,
                      f.CreatedDate,
                      f.Description,
                      f.Status,
                      CreatorFullname = m.Fullname,
                  })
               .Join(
                   _context.Manager,
                   f => f.UpdatedBy,
                  m => m.Id,
                  (f, m) => new ExtendedMembership
                  {
                      Id = f.Id,
                      Name = f.Name,
                      PointRate = f.PointRate,
                      PointAmount = f.PointAmount,
                      UpdatedBy = f.UpdatedBy,
                      UpdatedDate = f.UpdatedDate,
                      CreatedBy = f.CreatedBy,
                      CreatedDate = f.CreatedDate,
                      Description = f.Description,
                      Status = f.Status,
                      CreatorFullname = f.CreatorFullname,
                      UpdaterFullname = m.Fullname,
                  }
               ).FirstOrDefaultAsync();

        }
    }
}
