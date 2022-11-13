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
    public interface ITutorReportService : IBaseService<TutorReportDto>
    {
        Task<Response<ExtendedTutorReportDto>> Filter(TutorReportParameter parameter);
        Task<CusResponse> Accept(TutorReportDto dto);
        Task<CusResponse> Deny(TutorReportDto dto);
        Task<int> CountPending();
    }
}
