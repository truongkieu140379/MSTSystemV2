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
    public interface ISubjectService : IBaseService<SubjectDto>
    {
        Task<IEnumerable<SubjectDto>> GetByManagerId(int managerId);
        Task<ExtendedSubjectDto> GetExtendedSubjectById(int id);
        Task<IEnumerable<SubjectDto>> GetByTutor(int tutorId);
        Task<IEnumerable<SubjectDto>> GetByStatus(string status);
        Task<bool> CheckExist(string name);
        Task<IEnumerable<ExtendedSubjectDto>> GetAllExtendedSubject();
        Task Deactive(int tutorId, int managerId);
        Task<Response<ExtendedSubjectDto>> Filter(SubjectParameter parameter);
        Task Active(int id, int managerId);
    }
}
