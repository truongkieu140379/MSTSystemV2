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
    interface ICourseDetailRepository : IGenericRepository<CourseDetail>
    {
        Task<IEnumerable<CourseDetail>> GetByCourse(int courseId);
    }
}
