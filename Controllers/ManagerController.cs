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
    [Route("api/managers")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _service;

        public ManagerController(IManagerService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        // GET: api/<AccountController>
        [HttpGet("all")]
        public async Task<IEnumerable<ManagerDto>> Get()
        {
            return await _service.GetAll();
        }
        [AllowAnonymous]
        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ManagerDto>> Get(int id)
        {
            ManagerDto dto = await _service.GetById(id);
            if (dto == null)
            {
                return NotFound();
            }
            return dto;
        }
        [AllowAnonymous]
        //get manager by roleId (admin or managerRoleId)
        [HttpGet("get-by-role/{roleId}")]
        public async Task<ActionResult<IEnumerable<ExtendedManagerDto>>> GetByRoleId(int roleId)
        {
            try
            {
                var managers = await _service.GetByRoleId(roleId);
                if (managers.Any())
                {
                    return Ok(managers);
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
        public async Task<ActionResult<ManagerDto>> Post(ManagerDto dto)
        {
            await _service.Insert(dto);
            return NoContent();
        }
       
        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ManagerDto dto)
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
        [AllowAnonymous]
        [HttpGet("email/{email}")]
        public async Task<ActionResult<ManagerDto>> GetByActiveEmail(String email)
        {
            ManagerDto dto = await _service.GetByEmail(email);
            if (dto == null)
            {
                return Ok(false);
            }
            return Ok(dto);
        }
        [AllowAnonymous]
        [HttpGet("filter")]
        public async Task<ActionResult<Response<ManagerDto>>> Filter([FromQuery] ManagerParameter parameter)
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
        [AllowAnonymous]
        [HttpGet("get-by-status")]
        public async Task<ActionResult<IEnumerable<ManagerDto>>> GetByStatus(string status)
        {
            var result = await _service.GetByStatus(status, 2);
            if (result.Any())
            {
                return Ok(result);
            }
            return NotFound();
        }
        [AllowAnonymous]
        [HttpGet("get-manager-email-by-subject/{subjectId}")]
        public async Task<ActionResult<string>> GetManagerEmailBySubject(int subjectId)
        {
            try
            {
                var result = await _service.GetManagerEmailBySubject(subjectId);
                if (result != "")
                {
                    return Ok(result);
                } else return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
