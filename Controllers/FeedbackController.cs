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
    [Route("api/feedbacks")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _service;

        public FeedbackController(IFeedbackService service)
        {
            _service = service;
        }

        // GET: api/<AccountController>
        [HttpGet("all")]
        public async Task<IEnumerable<ExtendedFeedbackDto>> GetAll()
        {
            return await _service.GetAllExtended();
        }

        [HttpGet("manager/{managerId}")]
        public async Task<ActionResult<IEnumerable<ExtendedFeedbackDto>>> GetByManagerId(int managerId)
        {
            try
            {
                var courses = await _service.GetByManagerId(managerId);
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


        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedbackDto>> Get(int id)
        {
            FeedbackDto dto = await _service.GetById(id);
            if (dto == null)
            {
                return NotFound();
            }
            return dto;
        }

        //return tutor is feedback does not exist of this tutee and tutor;
        [HttpGet("check-exist/{result}")]
        public async Task<ActionResult<TutorDto>> Search(int tuteeId)
        {
            TutorDto dto = await _service.Search(tuteeId);
            if (dto == null)
            {
                return NotFound();
            }
            return dto;
        }

   
        [HttpPost]
        public async Task<ActionResult<FeedbackDto>> Post(FeedbackDto dto)
        {
            await _service.Insert(dto);
            return NoContent();
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, FeedbackDto dto)
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


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {      
            var result = await _service.Delete(id);
            return Ok(result);
        }

        [HttpGet("tutor/{tutorId}")]
        public async Task<ActionResult<IEnumerable<ExtendedFeedbackDto>>> GetByTutorId(int tutorId)
        {
            try
            {
                var feedbacks = await _service.GetByTutorId(tutorId);
                if (feedbacks.Any())
                {
                    return Ok(feedbacks);
                }
                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPut("active/{id}")]
        public async Task<ActionResult<bool>> ActiveFeedback(int id, FeedbackDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
     
               return await _service.Active(id, dto);
        }
    }
}
