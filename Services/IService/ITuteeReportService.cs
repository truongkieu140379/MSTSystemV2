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
    public interface ITuteeReportService :IBaseService<TuteeReportDto>
    {
        Task<Response<ExtendedTuteeReportDto>> Filter(TuteeReportParameter parameter);
        Task<CusResponse> Deny(TuteeReportDto dto);
        Task<CusResponse> Accept(TuteeReportDto dto);
        Task<int> CountPending();
    }
}
