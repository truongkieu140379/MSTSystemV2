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
    public interface IClassSubjectService
    {
        Task Insert(ClassHasSubjectDto dto);
        Task<ClassHasSubjectDto> Search(int subjectId, int classId);
        Task<IEnumerable<ExtendedClassHasSubjectDto>> GetAllExteded();
        Task<IEnumerable<ExtendedClassHasSubjectDto>> GetBySubject(int subjectId);
        Task<int> CheckByClass(int id);
        Task<int> CheckBySubject(int id);
        Task<bool> Inactive(int id);
        Task<Message> Active(int id);
        Task<Response<ExtendedClassHasSubjectDto>> Filter(ClassHasSubjectParameter parameter);
    }
}
