using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Dtos.ExtendedDtos;

namespace TutorSearchSystem.Services.IService
{
    public interface IEnrollmentService : IBaseService<EnrollmentDto>
    {
        Task<EnrollmentDto> Get(int courseId, int tuteeId);
        Task<bool> CheckFullCourse(int courseId);
        Task<IEnumerable<ExtendedEnrollmentDto>> GetEnrollmentsByTutorId(int tutorId, DateTime toDate);
    }
}
