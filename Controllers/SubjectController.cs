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
using TutorSearchSystem.Dtos.ExtendedDtos;
using TutorSearchSystem.Services.IService;

namespace TutorSearchSystem.Controllers
{
    [Authorize]
    [Route("api/subjects")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _service;

        public SubjectController(ISubjectService service)
        {
            _service = service;
        }

        // GET: api/<AccountController>
        [HttpGet("all")]
        public async Task<IEnumerable<ExtendedSubjectDto>> Get()
        {
            return await _service.GetAllExtendedSubject();
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExtendedSubjectDto>> Get(int id)
        {
            ExtendedSubjectDto dto = await _service.GetExtendedSubjectById(id);
            if (dto == null)
            {
                return NotFound();
            }
            return dto;
        }

        //get subject by manager id
        [HttpGet("subject-by-manager/{managerId}")]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetByManagerId(int managerId)
        {
            try
            {
                var subjects = await _service.GetByManagerId(managerId);
                if (subjects.Any())
                {
                    return Ok(subjects);
                }
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get subject by manager id
        [HttpGet("tutor/{tutorId}")]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetByTutor(int tutorId)
        {
            try
            {
                var subjects = await _service.GetByTutor(tutorId);
                if (subjects.Any())
                {
                    return Ok(subjects);
                }
                return Ok(null);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST api/<AccountController>
        [HttpPost]
        public async Task<ActionResult<SubjectDto>> Post(SubjectDto dto)
        {
            await _service.Insert(dto);
            return NoContent();
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, SubjectDto dto)
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
        [HttpPut("deactive/{param}")]
        public async Task<IActionResult> Deactive(int subjectId, int managerId)
        {
            try
            {
                await _service.Deactive(subjectId, managerId);
            }
            catch (DbUpdateConcurrencyException)
            {

                if (await _service.GetById(subjectId) == null)
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

        [HttpGet("check-exist")]
        public async Task<ActionResult<bool>> CheckExist(String name)
        {
            return await _service.CheckExist(name);

        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetByStatus(string status)
        {
            try
            {
                var subjects = await _service.GetByStatus(status);
                if (subjects.Any())
                {
                    return Ok(subjects);
                }
                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("filter")]
        public async Task<ActionResult<Response<ExtendedSubjectDto>>> Filter([FromQuery] SubjectParameter parameter)
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

        [HttpPut("active")]
        public async Task<IActionResult> Active(int subjectId, int managerId)
        {
            try
            {
                await _service.Active(subjectId, managerId);
            }
            catch (DbUpdateConcurrencyException)
            {

                if (await _service.GetById(subjectId) == null)
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
    }
}
