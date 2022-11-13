using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Filters;
using TutorSearchSystem.Models;
using TutorSearchSystem.Models.ExtendedModels;
using TutorSearchSystem.Global;

namespace TutorSearchSystem.Repositories.IRepositories
{

    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task<IEnumerable<CourseTutor>> Search(FilterQueryDto filter);
        Task<IEnumerable<ExtendedCourse>> Search(int tuteeId);
        Task<IEnumerable<Course>> Search(int tuteeId, int subjectId, int classId);
        Task<IEnumerable<CourseTutor>> GetActiveUnregisteredCourses(int tuteeId);
        Task<IEnumerable<ExtendedCourse>> Search(int tuteeId, string enrollmentStatus);
        Task<IEnumerable<Course>> Get(int tutorId, string status);
        Task<IEnumerable<ExtendedCourse>> GetCoursesByManagerId(int managerId);
        Task<IEnumerable<ExtendedCourse>> GetAllExtended();
        Task<Course> CheckValidate(CourseDto courseDto);
        Task<int> CheckCourseByTutorId(int tutorId);
        Task<PagedList<ExtendedCourse>> Filter(CourseParameter parameter);
        Task<int> CheckCourseByClassHasSubject(int id);
        Task<IEnumerable<CourseTutor>> GetByTutor(int tutorId);
    }
}
