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
    public interface ITutorService : IBaseService<TutorDto>
    {
        Task<TutorDto> Get(String email);
        Task<IEnumerable<ExtendedTutorDto>> GetForManager(int managerId);
        Task<IEnumerable<ExtendedTutorDto>> GetByStatus(string status);
        Task<bool> Deactive(int tutorId, int managerId);
        Task<IEnumerable<ExtendedTutorDto>> GetAllExtended();
        Task<ExtendedTutorDto> GetExtendedById(int id);
        Task<int> GetCountInMonth();
        Task<Response<ExtendedTutorDto>> Filter(TutorParameter parameter);
        Task<int> Count();
        Task<bool> Accept(TutorDto dto);
        Task<bool> Deny(TutorDto dto);
        Task<bool> Active(TutorDto dto);
        Task<bool> UpdateProfile(TutorDto dto);
    }
}
