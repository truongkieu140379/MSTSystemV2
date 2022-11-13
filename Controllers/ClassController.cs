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

namespace TutorSearchSystem.Controllers
{
    [Authorize]
    [Route("api/classes")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _service;

        public ClassController(IClassService service)
        {
            _service = service;
        }

        // GET: api/<AccountController>
        [HttpGet("all")]
        public async Task<IEnumerable<ClassDto>> Get()
        {
            return await _service.GetAll();
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExtendedClassDto>> GetExtendedClassById(int id)
        {
            ExtendedClassDto dto = await _service.GetExtendedClassById(id);
            if (dto == null)
            {
                return NotFound();
            }
            return dto;
        }

        //get class by subject id controller
        [HttpGet("subject/status/{subjectId}/{status}")]
        public async Task<ActionResult<ClassDto>> Search(int subjectId, string status)
        {
            try
            {
                var dtos = await _service.Search(subjectId, status);
                if (dtos.Any())
                {
                    return Ok(dtos);
                }
                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST api/<AccountController>
        [HttpPost]
        public async Task<ActionResult<ClassDto>> Post(ClassDto dto)
        {
            await _service.Insert(dto);
            return NoContent();
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ClassDto dto)
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

        [HttpPut("deactive/{param}")]
        public async Task<IActionResult> Deactive(int classId, int managerId)
        {
            try
            {
                await _service.Deactive(classId, managerId);
            }
            catch (DbUpdateConcurrencyException)
            {

                if (await _service.GetById(classId) == null)
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

        [HttpGet("filter")]
        public async Task<ActionResult<Response<ClassDto>>> Filter([FromQuery] ClassParameter parameter)
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
        [HttpGet("get-by-class-has-subject-not-exist")]
        public async Task<ActionResult<IEnumerable<ClassDto>>> GetByClassHasSubjectNotExist(int subjectId)
        {
            var result = await _service.GetByClassHasSubjectNotExist(subjectId);
            if (result.Any())
            {
                return Ok(result);
            }
            return NoContent();
        }
    }
}
