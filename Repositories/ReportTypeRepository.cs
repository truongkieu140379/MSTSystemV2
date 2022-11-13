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
    public class ReportTypeRepository :  GenericRepository<ReportType>, IReportTypeRepository
    {
        private readonly TSDbContext _context;
        public ReportTypeRepository(TSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedList<ExtendedReportType>> Filter(ReportTypeParameter parameter)
        {
            var entities = await _context.ReportType.Where(r => r.Name.Contains(parameter.Name))
                .Select(r => new ExtendedReportType
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    CreatedBy = r.CreatedBy,
                    CreatedDate = r.CreatedDate,
                    CreatorName = r.ReportTypeCreator.Fullname,
                    UpdatedBy = r.UpdatedBy,
                    UpdatedDate = r.UpdatedDate,
                    UpdatorName = r.ReportTypeUpdater.Fullname,
                    Status = r.Status,
                    RoleName = r.Role.Name,
                    RoleId = r.RoleId
                })
                .OrderByDescending(r => r.CreatedDate).ToListAsync();
            if (!String.IsNullOrEmpty(parameter.Status))
            {
                entities = entities.Where(r => r.Status == parameter.Status).ToList();
            }
            if (parameter.RoleId > 0)
            {
                entities = entities.Where(r => r.RoleId == parameter.RoleId).ToList();
            }
            return PagedList<ExtendedReportType>.ToPagedList(entities, parameter.PageNumber, parameter.PageSize);
        }

        public async Task<IEnumerable<ReportType>> GetByRole(int roleId)
        {
            return await _context.ReportType.Where(r => r.RoleId == roleId && r.Status == GlobalConstants.ACTIVE_STATUS).OrderBy(r => r.Name).ToListAsync();
        }
    }
}
