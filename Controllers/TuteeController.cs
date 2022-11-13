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

namespace TutorSearchSystem.Controllers
{
    [Authorize]
    [Route("api/tutees")]
    [ApiController]
    public class TuteeController : ControllerBase
    {
        private readonly ITuteeService _service;

        public TuteeController(ITuteeService service)
        {
            _service = service;
        }

        // GET: api/<AccountController>
        [HttpGet("all")]
        public async Task<IEnumerable<TuteeDto>> Get()
        {
            return await _service.GetAll();
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TuteeDto>> Get(int id)
        {
            TuteeDto dto = await _service.GetById(id);
            if (dto == null)
            {
                return NotFound();
            }
            return dto;
        }

        // GET tutee by email
        [HttpGet("email/{email}")]
        public async Task<ActionResult<TuteeDto>> Get(String email)
        {
            TuteeDto dto = await _service.Get(email);
            if (dto == null)
            {
                return NotFound();
            }
            return dto;
        }

        //get tutees in a course;
        [HttpGet("tutee-in-course/{courseId}")]
        public async Task<ActionResult<IEnumerable<TuteeDto>>> GetTuteeInCourse(int courseId)
        {
            try
            {
                var tutees = await _service.GetTuteeInCourse(courseId);
                if (tutees.Any())
                {
                    return Ok(tutees);
                }
                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
           [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<TuteeDto>> Post(TuteeDto dto)
        {
            await _service.Insert(dto);
            return NoContent();
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TuteeDto dto)
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

        ////remove here means update status to "Inactive" value
        [HttpPut("inactive/{id}")]
        public async Task<ActionResult<CusResponse>> Inactive(int id)
        {
            
            var result = await _service.Inactive(id);
            return result;
            
        }
        [HttpPut("active/{id}")]
        public async Task<ActionResult<CusResponse>> Active(int id)
        {
            
            var result = await _service.Active(id);
            return result;
            
        }

        [HttpGet("get-count-tutee-in-month")]
        public async Task<ActionResult<int>> GetCountInMonth()
        {
            return Ok(await _service.GetCountInMonth());
        }


        [HttpGet("filter")]
        public async Task<ActionResult<Response<TuteeDto>>> Filter([FromQuery] TuteeParameter parameter)
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
    }
}
