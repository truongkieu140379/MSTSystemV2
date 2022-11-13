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
    public interface IManagerService : IBaseService<ManagerDto>
    {
        Task<IEnumerable<ExtendedManagerDto>> GetByRoleId(int roleId);
        Task<ManagerDto> GetByEmail(String email);
        Task<Response<ManagerDto>> Filter(ManagerParameter parameter);
        Task<IEnumerable<ManagerDto>> GetByStatus(string status, int role);
        Task<string> GetManagerEmailBySubject(int subjectId);
    }
}
