using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Models;
using TutorSearchSystem.Models.ExtendedModels;

namespace TutorSearchSystem.Repositories.IRepositories
{
    public interface IEnrollmentRepository : IGenericRepository<Enrollment>
    {
        Task<Enrollment> Get(int courseId, int tuteeId);
        Task<int> CountEnrollmentByCourseId(int courseId);
        Task<IEnumerable<ExtendedEnrollment>> GetEnrollmentsByTutorId(int tutorId, DateTime toDate);
        Task<int> CountEnrollmentByCourseIdWithStatus(string status, int courseId);
    }
}
