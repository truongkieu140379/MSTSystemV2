using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Models;
using TutorSearchSystem.Global;
using TutorSearchSystem.Filters;
using TutorSearchSystem.Models.ExtendedModels;

namespace TutorSearchSystem.Repositories.IRepositories
{
    public interface IClassSubjectRepository : IGenericRepository<ClassHasSubject>
    {
        Task<ClassHasSubject> Search(int subjectId, int classId);
        Task<IEnumerable<ExtendedClassHasSubject>> GetAllExteded();
        Task<IEnumerable<ExtendedClassHasSubject>> GetBySubject(int subjectId);
        Task<int> CheckBySubject(int id);
        Task<int> CheckByClass(int id);
        Task<PagedList<ExtendedClassHasSubject>> Filter(ClassHasSubjectParameter parameter);
    }
}
