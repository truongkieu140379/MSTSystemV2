using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Models;
using TutorSearchSystem.Global;
using TutorSearchSystem.Filters;
using TutorSearchSystem.Models.ExtendedModels;

namespace TutorSearchSystem.Repositories.IRepositories
{
    public interface IReportTypeRepository :IGenericRepository<ReportType>
    {
        Task<IEnumerable<ReportType>> GetByRole(int roleId);
        Task<PagedList<ExtendedReportType>> Filter(ReportTypeParameter parameter);
    }
}
