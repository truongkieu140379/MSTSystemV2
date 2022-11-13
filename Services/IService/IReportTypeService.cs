using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TutorSearchSystem.Dtos;
using TutorSearchSystem.Global;
using TutorSearchSystem.Filters;
using TutorSearchSystem.Dtos.ExtendedDtos;

namespace TutorSearchSystem.Services.IService
{
    public interface IReportTypeService : IBaseService<ReportTypeDto>
    {
        Task<IEnumerable<ReportTypeDto>> GetByRole(int roleId);
        Task<Response<ExtendedReportTypeDto>> Filter(ReportTypeParameter parameter);
        Task<CusResponse> Inactive(ReportTypeDto dto);
        Task<CusResponse> Active(ReportTypeDto dto);
    }
}
