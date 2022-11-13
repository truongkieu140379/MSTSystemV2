using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Global;
using TutorSearchSystem.Filters;
using TutorSearchSystem.Services.IService;
using TutorSearchSystem.Dtos.ExtendedDtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TutorSearchSystem.Controllers
{
    [Authorize]
    [Route("api/course-detail")]
    [ApiController]
    public class CourseDetailController : ControllerBase
    {
        private readonly ICourseDetailService _service;
        
        public CourseDetailController(ICourseDetailService service)
        {
            _service = service;
        }
        // GET: api/<CourseDetailController>
        [HttpGet]
        public async Task<IEnumerable<CourseDetailDto>> Get()
        {
            return await _service.GetAll();
        }

        // GET api/<CourseDetailController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDetailDto>> Get(int id)
        {
            var entity = await _service.GetById(id);
            if(entity!= null)
            {
                return Ok(entity);
            }
            return NoContent();
        }

        [HttpGet("get-by-course/{id}")]
        public async Task<ActionResult<IEnumerable<CourseDetailDto>>> GetByCourse(int id)
        {
            var entities = await _service.GetByCourse(id);
            return Ok(entities);
        }

        // POST api/<CourseDetailController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CourseDetailDto dto)
        {
            await _service.Insert(dto);
            return NoContent();
        }

        // PUT api/<CourseDetailController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CourseDetailDto dto)
        {
            if(id != dto.Id)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    await _service.Update(dto);
                    return NoContent();
                }
                catch (Exception)
                {
                    return NoContent();
                }
            }
        }
        [HttpDelete("delete-by-course/{id}")]
        public async Task<ActionResult> DeleteByCourse(int id)
        {
            
                    await _service.DeleteByCourse(id);
                    return NoContent();
       
        }
    }
}
