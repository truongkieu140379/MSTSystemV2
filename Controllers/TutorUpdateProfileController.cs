using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Services.IService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TutorSearchSystem.Controllers
{
    [Authorize]
    [Route("api/tutor-update-profile")]
    [ApiController]
    public class TutorUpdateProfileController : ControllerBase
    {
        private readonly ITutorUpdateProfileService _service;

        public TutorUpdateProfileController(ITutorUpdateProfileService service)
        {
            _service = service;
        }

        // GET: api/<AccountController>
        [HttpGet("all")]
        public async Task<IEnumerable<TutorUpdateProfileDto>> Get()
        {
            return await _service.GetAll();
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TutorUpdateProfileDto>> Get(int id)
        {
            TutorUpdateProfileDto dto = await _service.GetById(id);
            if (dto == null)
            {
                return NotFound();
            }
            return dto;
        }

        // POST api/<AccountController>
        [HttpPost]
        public async Task<ActionResult<TutorUpdateProfileDto>> Post(TutorUpdateProfileDto dto)
        {
            await _service.Insert(dto);
            return NoContent();
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TutorUpdateProfileDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            try
            {
                if (await _service.GetById(id) == null)
                {
                    return NotFound();
                }
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
      
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {          
             return await _service.Delete(id);

        }
    }
}

