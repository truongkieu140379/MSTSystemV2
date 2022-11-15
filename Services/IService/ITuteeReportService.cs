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
    public interface ITuteeReportService :IBaseService<CustomerReportDto>
    {
        Task<Response<ExtendedCustomerReportDto>> Filter(TuteeReportParameter parameter);
        Task<CusResponse> Deny(CustomerReportDto dto);
        Task<CusResponse> Accept(CustomerReportDto dto);
        Task<int> CountPending();
    }
}
