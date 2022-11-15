using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Global;
using TutorSearchSystem.Filters;

namespace TutorSearchSystem.Services.IService
{

    public interface ITuteeService : IBaseService<CustomerDto>
    {
        Task<CustomerDto> Get(String email);
        Task<IEnumerable<CustomerDto>> GetTuteeInCourse(int courseId);
        Task<Response<CustomerDto>> Filter(TuteeParameter parameter);
        Task<int> GetCountInMonth();
        Task<CusResponse> Active(int id);
        Task<CusResponse> Inactive(int id);
    }
}
