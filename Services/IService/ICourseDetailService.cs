using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;

namespace TutorSearchSystem.Services.IService
{
    public interface ICourseDetailService: IBaseService<CourseDetailDto>
    {
        Task<IEnumerable<CourseDetailDto>> GetByCourse(int courseId);
        Task DeleteByCourse(int courseId);
    }
}
