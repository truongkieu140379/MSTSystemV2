using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Models;
using TutorSearchSystem.Global;
using TutorSearchSystem.Filters;

namespace TutorSearchSystem.Repositories.IRepositories
{
    public interface ITuteeRepository : IGenericRepository<Tutee>
    {
        Task<Tutee> Get(String email);
        Task<IEnumerable<Tutee>> GetTuteeInCourse(int courseId);
        Task<PagedList<Tutee>> Filter(TuteeParameter parameter);
        Task<int> GetCountInMonth();
    }
}
