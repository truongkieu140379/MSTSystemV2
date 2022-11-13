using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Filters;
using TutorSearchSystem.Global;
using TutorSearchSystem.Dtos.ExtendedDtos;
using TutorSearchSystem.Services.IService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TutorSearchSystem.Controllers
{
    [Authorize]
    [Route("api/class-has-subject")]
    [ApiController]
    public class ClassHasSubjectController : ControllerBase
    {
        private readonly IClassSubjectService _service;

        public ClassHasSubjectController(IClassSubjectService service)
        {
            _service = service;
        }

        // GET all class has subject
        [HttpGet("all")]
        public async Task<ActionResult<ExtendedClassHasSubjectDto>> GetAll()
        {
            try
            {
                var dtos = await _service.GetAllExteded();
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

        // GET api/<AccountController>/5
        [HttpGet("search/{result}")]
        public async Task<ActionResult<ClassHasSubjectDto>> Search(int subjectId, int classId)
        {
            ClassHasSubjectDto dto = await _service.Search(subjectId, classId);
            if (dto == null)
            {
                return Ok(null);
            }
            return dto;
        }

        //get class by subject id controller
        [HttpGet("subject/{subjectId}")]
        public async Task<ActionResult<ExtendedClassHasSubjectDto>> GetBySubject(int subjectId)
        {
            try
            {
                var dtos = await _service.GetBySubject(subjectId);
                if (dtos.Any())
                {
                    return Ok(dtos);
                }
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST api/<AccountController>
        [HttpPost]
        public async Task<ActionResult<ClassHasSubjectDto>> Post(ClassHasSubjectDto dto)
        {
            await _service.Insert(dto);
            return NoContent();
        }

        [HttpGet("check-by-class")]
        public async Task<ActionResult<int>> CheckByClass(int id)
        {
            return await _service.CheckByClass(id);
        }
        [HttpGet("check-by-subject")]
        public async Task<ActionResult<int>> CheckBySubject(int id)
        {
            return await _service.CheckBySubject(id);
        }
        [HttpPut("inactive/{id}")]
        public async Task<ActionResult>Inactive(int id)
        {
            try
            {
                bool result = await _service.Inactive(id);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
             
        }
        [HttpPut("active/{id}")]
        public async Task<ActionResult<Message>> Active(int id)
        {
            try
            {
                var result = await _service.Active(id);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpGet("filter")]
        public async Task<ActionResult<Response<ExtendedClassHasSubjectDto>>> Filter([FromQuery]ClassHasSubjectParameter parameter)
        {
            try
            {
                var result = await _service.Filter(parameter);
                if (result.Data.Any())
                {
                    return Ok(result);
                }
                else return Ok(null);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
