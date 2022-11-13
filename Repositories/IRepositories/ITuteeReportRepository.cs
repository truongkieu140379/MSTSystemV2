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
    public interface ITuteeReportRepository : IGenericRepository<TuteeReport>
    {
        Task<PagedList<ExtendedTuteeReport>> Filter(TuteeReportParameter parameter);
        Task<int> CountPending();
    }
}
