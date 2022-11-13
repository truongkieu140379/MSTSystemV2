using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Global;
using TutorSearchSystem.Filters;

namespace TutorSearchSystem.Services.IService
{

    public interface ITuteeService : IBaseService<TuteeDto>
    {
        Task<TuteeDto> Get(String email);
        Task<IEnumerable<TuteeDto>> GetTuteeInCourse(int courseId);
        Task<Response<TuteeDto>> Filter(TuteeParameter parameter);
        Task<int> GetCountInMonth();
        Task<CusResponse> Active(int id);
        Task<CusResponse> Inactive(int id);
    }
}
