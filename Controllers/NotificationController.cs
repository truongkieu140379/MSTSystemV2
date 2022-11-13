using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.FCMHelpers;
using TutorSearchSystem.Services.IService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TutorSearchSystem.Controllers
{
   // [Authorize]
    [Route("api/notifications")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;

        public NotificationController(INotificationService service)
        {
            _service = service;
        }

        // GET: api/<AccountController>
        [HttpGet("all")]
        public async Task<IEnumerable<NotificationDto>> Get()
        {
            return await _service.GetAll();
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationDto>> Get(int id)
        {
            NotificationDto dto = await _service.GetById(id);
            if (dto == null)
            {
                return NotFound();
            }
            return dto;
        }

        // POST api/<AccountController>
        [HttpPost]
        public async Task<ActionResult<NotificationDto>> Post(NotificationDto dto)
        {
            await _service.Insert(dto);
            return NoContent();
        }

        [HttpPost("all-manager")]
        public async Task<IActionResult> SendNotificationToAllManager(string title, string message)
        {
            await _service.SendNotificationToAllManager(title, message);
            return NoContent();
        }

        [HttpPost("send-admin")]
        public async Task<IActionResult> SendNotificationToAdmin(string title, string message)
        {
            await _service.SendNotificationToAdmin(title, message);
            return NoContent();
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, NotificationDto dto)
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


        [HttpGet("email/{email}")]
        public async Task<ActionResult<IEnumerable<NotificationDto>>> GetNotificationByEmail(string email)
        {
            try
            {
                var courses = await _service.GetNotificationByEmail(email);
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
    }
}
