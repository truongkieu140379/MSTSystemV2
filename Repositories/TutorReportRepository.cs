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
    public class TutorReportRepository : GenericRepository<TutorReport>, ITutorReportRepository
    {
        private readonly TSDbContext _context;
        public TutorReportRepository(TSDbContext context): base(context)
        {
            _context = context;
        }

        public Task Delete(TuteeReport entity)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<ExtendedTutorReport>> Filter(TutorReportParameter parameter)
        {
            var entities = await _context.TutorReport.Where(t => (t.CreatedDate >= parameter.FromDate && t.CreatedDate <= parameter.ToDate)
            && t.Tutor.Email.Contains(parameter.TutorEmail))
                .Select(t => new ExtendedTutorReport
                {
                    Id = t.Id,
                    ConfirmedBy = t.ConfirmedBy,
                    ConfirmedDate = t.ConfirmedDate,
                    ConfirmName = t.Manager.Fullname,
                    Image = t.Image,
                    Status = t.Status,
                    Description = t.Description,
                    TutorName = t.Tutor.Fullname,
                    ReportName = t.ReportType.Name,
                    CreatedDate= t.CreatedDate,
                    TutorEmail = t.Tutor.Email
                }).OrderByDescending(t => t.CreatedDate).ToListAsync();
            if (!String.IsNullOrEmpty(parameter.Status))
            {
                entities = entities.Where(t => t.Status == parameter.Status).ToList();
            }
            return PagedList<ExtendedTutorReport>.ToPagedList(entities, parameter.PageNumber, parameter.PageSize);
        }

        public async Task<int> CountPending()
        {
            var result = await _context.TutorReport.Where(t => t.Status == GlobalConstants.PENDING_STATUS).ToListAsync();
            return result.Count;
        }
    }
}
