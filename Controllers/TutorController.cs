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
    [Route("api/tutors")]
    [ApiController]
    public class TutorController : ControllerBase
    {
        private readonly ITutorService _service;

        public TutorController(ITutorService service)
        {
            _service = service;
        }

        // GET: api/<AccountController>
        [HttpGet("all")]
        public async Task<IEnumerable<ExtendedTutorDto>> GetAll()
        {
            return await _service.GetAllExtended();
        }

        //[HttpGet("test")]
        //public ExtendedTutorDto GetTest()
        //{

        //    string[] result = { "one", "two" };
        //    result.ToArray();

        //    return new ExtendedTutorDto
        //    {
        //        Names = result,
        //        ConfirmerName = "This is test"
        //    };
        //}

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExtendedTutorDto>> GetExtendedById(int id)
        {
            ExtendedTutorDto dto = await _service.GetExtendedById(id);
            if (dto == null)
            {
                return NotFound();
            }
            return dto;
        }

        // GET tutor by email
        [HttpGet("email/{email}")]
        public async Task<ActionResult<TutorDto>> Get(String email)
        {
            TutorDto dto = await _service.Get(email);
            if (dto == null)
            {
                return NotFound();
            }
            return dto;
        }

        //get tutors by status and managerId
        [HttpGet("manager/{managerId}")]
        public async Task<ActionResult<IEnumerable<ExtendedTutorDto>>> GetForManger(int managerId)
        {
            try
            {
                IEnumerable<ExtendedTutorDto> dtos;

                dtos = await _service.GetForManager(managerId);

                if (dtos.Any())
                {
                    return Ok(dtos);
                }
                return Ok(null);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get tutors by status and managerId
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<ExtendedTutorDto>>> GetByStatus(string status)
        {
            try
            {
                IEnumerable<ExtendedTutorDto> dtos = await _service.GetByStatus(status);

                if (dtos.Any())
                {
                    return Ok(dtos);
                }
                return Ok(null);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<TutorDto>> Post(TutorDto dto)
        {
            await _service.Insert(dto);
            return NoContent();
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(int id, TutorDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            try
            {
                return await _service.UpdateProfile(dto);
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
        }

        ////remove here means update status to "Inactive" value
       
        [HttpPut("deactive/{param}")]
        public async Task<ActionResult<bool>> Deactive(int tutorId, int managerId)
        {
            try
            {
                return await _service.Deactive(tutorId, managerId);
            }
            catch (DbUpdateConcurrencyException)
            {

                if (await _service.GetById(tutorId) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
        [HttpGet("get-count-tutor-in-month")]
        public async Task<ActionResult<int>> GetCountInMonth()
        {
            return Ok(await _service.GetCountInMonth());
        }

        [HttpGet("filter")]
        public async Task<ActionResult<Response<ExtendedTutorDto>>> Filter([FromQuery]TutorParameter parameter)
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
        [HttpGet("count")]
        public async Task<ActionResult<int>> Count()
        {
            return  Ok(await _service.Count());
        }
  
        [HttpPut("accept/{id}")]
        public async Task<ActionResult<bool>> Accept(int id, TutorDto dto)
        {
            if(id != dto.Id)
            {
                return BadRequest();
            }
            return await _service.Accept(dto);
        }
   
        [HttpPut("deny/{id}")]
        public async Task<ActionResult<bool>> Deny(int id, TutorDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            return await _service.Deny(dto);
        }
      
        [HttpPut("active/{id}")]
        public async Task<ActionResult<bool>> Active(int id, TutorDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            return await _service.Active(dto);
        }
    }
}
