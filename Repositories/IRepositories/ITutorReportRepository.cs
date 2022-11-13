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
    public interface ITutorReportRepository
    {
        Task<PagedList<ExtendedTutorReport>> Filter(TutorReportParameter parameter);
        Task<int> CountPending();
    }
}
