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
    public class FeeRepository : GenericRepository<Fee>, IFeeRepository
    {
        private readonly TSDbContext _context;

        public FeeRepository(TSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckExist(string name)
        {
            return await _context.Fee.AnyAsync(f => f.Name == name);
        }

        public async Task<IEnumerable<ExtendedFee>> GetAllExtendedFee()
        {
            return await _context.Fee.Join(
                _context.Manager,
                   f => f.CreatedBy,
                   m => m.Id,
                   (f, m) => new
                   {
                       f.Id,
                       f.Name,
                       f.Price,
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
                   (f, m) => new ExtendedFee
                   {
                       Id = f.Id,
                       Name = f.Name,
                       Price = f.Price,
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

        public async Task<IEnumerable<Fee>> GetByStatus(string status)
        {
            return await _context.Fee.Where(f => f.Status == status).ToListAsync();
        }

        public async Task<ExtendedFee> GetExtendedById(int id)
        {
            return await _context.Fee.Join(
                _context.Manager,
                   f => f.CreatedBy,
                   m => m.Id,
                   (f, m) => new
                   {
                       f.Id,
                       f.Name,
                       f.Price,
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
                   (f, m) => new ExtendedFee
                   {
                       Id = f.Id,
                       Name = f.Name,
                       Price = f.Price,
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
                .Where(f => f.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
