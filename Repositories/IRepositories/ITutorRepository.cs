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
    public interface ITutorRepository : IGenericRepository<Tutor>
    {
        Task<Tutor> Get(String email);
        Task<IEnumerable<ExtendedTutor>> GetForManager(int managerId);
        Task<IEnumerable<ExtendedTutor>> GetByStatus(string status);
        Task<IEnumerable<ExtendedTutor>> GetAllExtended();
        Task<ExtendedTutor> GetExtendedById(int id);
        Task<int> GetCountInMonth();
        Task<int> GetNumberOfCourseByTutorId(int tutorId);
        Task<int> GetNumberOfTuteeByTutorId(int tutorId);
        Task<int> GetNumberOfFeedbackByTutorId(int tutorId);
        Task<PagedList<ExtendedTutor>> Filter(TutorParameter parameter);
    }
}
