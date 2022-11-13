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
    [Route("api/fees")]
    [ApiController]
    public class FeeController : ControllerBase
    {
        private readonly IFeeService _service;

        public FeeController(IFeeService service)
        {
            _service = service;
        }

        // GET: api/<AccountController>
        [HttpGet("all")]
        public async Task<IEnumerable<ExtendedFeeDto>> GetAll()
        {
            return await _service.GetAllExtendedFee();
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExtendedFeeDto>> GetExtendedById(int id)
        {
            ExtendedFeeDto dto = await _service.GetExtendedById(id);
            if (dto == null)
            {
                return NotFound();
            }
            return dto;
        }

        //get course by search keys
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<FeeDto>>> GetByStatus(String status)
        {
            try
            {
                var fees = await _service.GetByStatus(status);
                if (fees.Any())
                {
                    return Ok(fees);
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
        public async Task<ActionResult<FeeDto>> Post(FeeDto dto)
        {
            await _service.Insert(dto);
            return NoContent();
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, FeeDto dto)
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

        [HttpGet("check-exist/{name}")]
        public async Task<ActionResult<bool>> CheckExist(String name)
        {
            return await _service.CheckExist(name);

        }
    }
}
