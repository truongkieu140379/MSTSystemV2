using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Dtos.ExtendedDtos;
using TutorSearchSystem.Services.IService;

namespace TutorSearchSystem.Controllers
{
    [Authorize]
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _service;

        public EnrollmentController(IEnrollmentService service)
        {
            _service = service;
        }

        // GET: api/<AccountController>
        [HttpGet("all")]
        public async Task<IEnumerable<EnrollmentDto>> Get()
        {
            return await _service.GetAll();
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EnrollmentDto>> Get(int id)
        {
            EnrollmentDto dto = await _service.GetById(id);
            if (dto == null)
            {
                return NotFound();
            }
            return dto;
        }

        // GET api/<AccountController>/5
        [HttpGet("course/tutee/{courseId}/{tuteeId}")]
        public async Task<ActionResult<EnrollmentDto>> Get(int courseId, int tuteeId)
        {
            EnrollmentDto dto = await _service.Get(courseId, tuteeId);
            if (dto == null)
            {
                return NotFound();
            }
            return dto;
        }

        // POST api/<AccountController>
        [HttpPost]
        public async Task<ActionResult<EnrollmentDto>> Post(EnrollmentDto dto)
        {
            await _service.Insert(dto);
            return NoContent();
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EnrollmentDto dto)
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

        [HttpPut("check-full-course/{courseId}")]
        public async Task<ActionResult<bool>> CheckFullCourse(int courseId)
        {
            try
            {
               return Ok(await _service.CheckFullCourse(courseId));
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        [HttpGet("tutorId/toDate/{tutorId}/{toDate}")]
        public async Task<ActionResult<IEnumerable<ExtendedEnrollmentDto>>> GetEnrollmentsByTutorId(int tutorId, DateTime toDate)
        {
            try
            {
                var enrollments = await _service.GetEnrollmentsByTutorId(tutorId, toDate);
                if (enrollments.Any())
                {
                    return Ok(enrollments);
                }
                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
