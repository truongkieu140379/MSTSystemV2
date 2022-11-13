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
    public interface IClassService : IBaseService<ClassDto>
    {
        Task<IEnumerable<ClassDto>> Search(int subjectId, string status);
        Task<bool> CheckExist(string name);
        Task Deactive(int classId, int managerId);
        Task<Response<ClassDto>> Filter(ClassParameter parameter);
        Task<ExtendedClassDto> GetExtendedClassById(int id);
        Task<IEnumerable<ClassDto>> GetByClassHasSubjectNotExist(int subjectId);
    }
}
