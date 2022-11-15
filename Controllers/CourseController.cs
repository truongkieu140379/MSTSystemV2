using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Dtos.ExtendedDtos;
using TutorSearchSystem.Filters;
using TutorSearchSystem.Global;
using TutorSearchSystem.Services.IService;

namespace TutorSearchSystem.Controllers
{
    [Authorize]
    [Route("api/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _service;

        public CourseController(ICourseService service)
        {
            _service = service;
        }

        // GET: api/<AccountController>
        [HttpGet("all")]
        public async Task<IEnumerable<ExtendedCourseDto>> GetAll()
        {
            return await _service.GetAllExtended();
        }

        // GET api/<AccountController>/5
        [HttpGet("get/{id}")]
        public async Task<ActionResult<ExtendedCourseDto>> GetExtendedCourseById(int id)
        {
            ExtendedCourseDto dto = await _service.GetExtendedCourseById(id);
            if (dto == null)
            {
                return NotFound();
            }
            return dto;
        }

        //get by id return extended course dto: contains class info and subject info
        [HttpGet("{id}")]
        public async Task<ActionResult<ExtendedCourseDto>> GetById(int id, int tuteeId)
        {
            ExtendedCourseDto dto = await _service.GetExtendedCourse(id, tuteeId);
            if (dto == null)
            {
                return NotFound();
            }
            return dto;
        }

        [HttpGet("manager/{managerId}")]
        public async Task<ActionResult<IEnumerable<ExtendedCourseDto>>> GetCoursesByManagerId(int managerId)
        {
            try
            {
                var courses = await _service.GetCoursesByManagerId(managerId);
                if (courses.Any())
                {
                    return Ok(courses);
                }
                return Ok(null);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get course by search keys
        [HttpGet("tutor/filter")]
        public async Task<ActionResult<IEnumerable<CourseTutorDto>>> Search([FromQuery] FilterQueryDto filterDto)
        {
            try
            {
                var courses = await _service.Search(filterDto);
                if (courses.Any())
                {
                    return Ok(courses);
                }
                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get course by tuteeId; we need to join enrollment table and course table; select course by tuteeId
        [HttpGet("courses-by-enrollment-status/{courses}")]
        public async Task<ActionResult<IEnumerable<ExtendedCourseDto>>> Search(int tuteeId, string enrollmentStatus)
        {
            try
            {
                IEnumerable<ExtendedCourseDto> courses = null;
                if (enrollmentStatus == "All")
                {
                    courses = await _service.Search(tuteeId);
                }
                else
                {
                    courses = await _service.Search(tuteeId, enrollmentStatus);
                }


                if (courses.Any())
                {
                    return Ok(courses);
                }
                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get course by tuteeId; we need to join enrollment table and course table; select course by tuteeId
        [HttpGet("tutor/{courses}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> Get(int tutorId, string status)
        {
            try
            {
                var courses = await _service.Get(tutorId, status);
                if (courses.Any())
                {
                    return Ok(courses);
                }
                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get course by customerId; we need to join enrollment table and course table; select course by customerId
        //then get course by subjectId and classId
        [HttpGet("customer/subject/class/{result}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> Search(int tuteeId, int subjectId, int classId)
        {
            try
            {
                var courses = await _service.Search(tuteeId, subjectId, classId);
                if (courses.Any())
                {
                    return Ok(courses);
                }
                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get COurse by tuteeId and enrollment status(Accepted, Denied, pending)
        //get course by tuteeId; we need to join enrollment table and course table; select course by tuteeId
        [HttpGet("courses-by-customer-id/{customerId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> Search(int tuteeId)
        {
            try
            {
                var courses = await _service.Search(tuteeId);
                if (courses.Any())
                {
                    return Ok(courses);
                }
                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get Courses have status active;
        //get courses doesn't register by specific tutee
        [HttpGet("customer-home/{customerId}/")]
        public async Task<ActionResult<IEnumerable<CourseTutorDto>>> GetActiveUnregisteredCourses(int tuteeId)
        {
            try
            {
                var courses = await _service.GetActiveUnregisteredCourses(tuteeId);
                if (courses.Any())
                {
                    return Ok(courses);
                }
                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public async Task<ActionResult<CourseDto>> Post(CourseDto dto)
        {
            await _service.Insert(dto);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CourseDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            try
            {
                await _service.Update(dto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _service.GetById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
            return NoContent();
        }

        //remove here means update status to "Inactive" value
        [HttpPut("inactive/{id}")]
        public async Task<IActionResult> Inactive(int id)
        {
            try
            {
                await _service.Inactive(id);
            }
            catch (DbUpdateConcurrencyException)
            {

                if (await _service.GetById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpPut("inactive")]
        public async Task<ActionResult<CusResponse>> InactiveWeb(int id, int confirmedId)
        {
            var result = await _service.InactiveCourse(id, confirmedId);
            return result;
        }


        //check whether or not begin/end date and begin/endtime co bi trung voi course da tao khong
        [HttpPost("check-validate")]
        public async Task<ActionResult<CourseDto>> CheckValidate(CourseDto courseDto)
        {

            var dto = await _service.CheckValidate(courseDto);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        [HttpGet("check-course-by-tutor/{tutorId}")]
        public async Task<int> CheckCourseByTutorId(int tutorId)
        {
            return await _service.CheckCourseByTutorId(tutorId);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<Response<ExtendedCourseDto>>> Filter([FromQuery]CourseParameter parameter)
        {
            try
            {
                var result = await _service.Filter(parameter);
                if (result.Data.Any())
                {
                    return Ok(result);
                }
                return Ok(null);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("check-course-by-class-has-subject")]
        public async Task<ActionResult<int>>CheckCourseByClassHasSubject([FromQuery]int id)
        {
            return await _service.CheckCourseByClassHasSubject(id);
        }

        [HttpPut("confirm/{id}")]
        public async Task<ActionResult<bool>> Confirm(int id, CourseDto dto)
        {
            if(id != dto.Id)
            {
                return BadRequest();
            }
            return await _service.ConfirmCourse(dto);
        }

        // get one course by tutor id and status pending newest
        [HttpGet("get-by-tutor-latest/{id}")]
        public async Task<ActionResult<CourseTutorDto>> GetByTutor(int id)
        {
            var result = await _service.GetByTutor(id);
            if (result != null)
            {
                return Ok(result);
            }
            else return NoContent();
        }
    }
}
