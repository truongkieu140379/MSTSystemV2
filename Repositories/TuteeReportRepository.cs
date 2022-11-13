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
    public class TuteeReportRepository : GenericRepository<TuteeReport>, ITuteeReportRepository
    {
        private readonly TSDbContext _context;
        public TuteeReportRepository(TSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountPending()
        {
            var result =  await _context.TuteeReport.Where(t => t.Status == GlobalConstants.PENDING_STATUS).ToListAsync();
            return result.Count;
        }

        public async Task<PagedList<ExtendedTuteeReport>> Filter(TuteeReportParameter parameter)
        {         
            var entities = await _context.TuteeReport.Where(t => 
            (t.CreatedDate >= parameter.FromDate && t.CreatedDate <= parameter.ToDate)
            && t.Enrollment.Tutee.Email.Contains(parameter.TuteeEmail))
                .Select(t => new ExtendedTuteeReport
                {
                    Id = t.Id,
                    Description = t.Description,
                    ConfirmedBy = t.ConfirmedBy,
                    ConfirmedDate = t.ConfirmedDate,
                    ConfirmName = t.Manager.Fullname,
                    TuteeName = t.Enrollment.Tutee.Fullname,
                    TutorName = t.Enrollment.Course.Tutor.Fullname,
                    CourseName = t.Enrollment.Course.Name,
                    CreatedDate = t.CreatedDate,
                    Image = t.Image,
                    ReportName = t.ReportType.Name,
                    Status= t.Status,
                    TuteeEmail = t.Enrollment.Tutee.Email,
                    TutorEmail = t.Enrollment.Course.Tutor.Email,
                    CourseId = t.Enrollment.CourseId
                })
                .OrderByDescending(t => t.CreatedDate).ToListAsync();
            if (!String.IsNullOrEmpty(parameter.Status))
            {
                entities = entities.Where(t => t.Status == parameter.Status).ToList();
            }
            if(parameter.ReportType != 0)
            {
                entities = entities.Where(t => t.ReportTypeId == parameter.ReportType).ToList();
            }
            return PagedList<ExtendedTuteeReport>.ToPagedList(entities, parameter.PageNumber, parameter.PageSize);
        }
    }
}
