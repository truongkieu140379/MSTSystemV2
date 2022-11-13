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
    public interface ISubjectRepository : IGenericRepository<Subject>
    {
        Task<IEnumerable<Subject>> GetByManagerId(int managerId);
        Task<IEnumerable<Subject>> GetByTutor(int tutorId);
        Task<IEnumerable<Subject>> GetByStatus(string status);
        Task<bool> CheckExist(string name);
        Task<IEnumerable<ExtendedSubject>> GetAllExtendedSubject();
        Task<ExtendedSubject> GetExtendedSubjectById(int id);
        Task<PagedList<ExtendedSubject>> Filter(SubjectParameter parameter);
    }
}
