using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Dtos.ExtendedDtos;
using TutorSearchSystem.Filters;
using TutorSearchSystem.Global;

namespace TutorSearchSystem.Services.IService
{
    public interface ICourseService : IBaseService<CourseDto>
    {
        Task<IEnumerable<CourseTutorDto>> Search(FilterQueryDto filterDto);
        Task<IEnumerable<ExtendedCourseDto>> GetCoursesByManagerId(int managerId);
        Task<IEnumerable<ExtendedCourseDto>> Search(int tuteeId);
        Task<IEnumerable<CourseTutorDto>> GetActiveUnregisteredCourses(int tuteeId);
        Task<IEnumerable<ExtendedCourseDto>> Search(int tuteeId, string enrollmentStatus);
        Task<IEnumerable<CourseDto>> Get(int tutor, string status);
        Task<IEnumerable<CourseDto>> Search(int tuteeId, int subjectId, int classId);
        Task<ExtendedCourseDto> GetExtendedCourse(int id, int tuteeId);
        Task<ExtendedCourseDto> GetExtendedCourseById(int id);
        Task<IEnumerable<ExtendedCourseDto>> GetAllExtended();
        Task<CourseDto> CheckValidate(CourseDto courseDto);
        Task<int> CheckCourseByTutorId(int tutorId);
        Task<Response<ExtendedCourseDto>> Filter(CourseParameter parameter);
        Task<int> CheckCourseByClassHasSubject(int id);
        Task<bool> ConfirmCourse(CourseDto dto);
        Task<CourseTutorDto> GetByTutor(int tutorId);
        Task<CusResponse> InactiveCourse(int courseId, int confirmedBy);
    }
}
