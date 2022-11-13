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


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TutorSearchSystem.Controllers
{
    [Authorize]
    [Route("api/report-type")]
    [ApiController]
    public class ReportTypeController : ControllerBase
    {
        private readonly IReportTypeService _service;
        public ReportTypeController(IReportTypeService service)
        {
            _service = service;
        }
        // GET: api/<ReportTypeController>
        [HttpGet("all")]
        public async Task<IEnumerable<ReportTypeDto>> Get()
        {
            return await _service.GetAll();
        }

        // GET api/<ReportTypeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportTypeDto>> Get(int id)
        {
            var entity = await _service.GetById(id);
            if(entity != null)
            {
                return entity;
            }
            return NoContent();
        }

        // POST api/<ReportTypeController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ReportTypeDto dto)
        {
            await _service.Insert(dto);
            return NoContent();
        }

        // PUT api/<ReportTypeController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ReportTypeDto dto)
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

        [HttpGet("filter")]
        public async Task<ActionResult<Response<ExtendedReportTypeDto>>> Filter([FromQuery]ReportTypeParameter parameter)
        {
            return Ok(await _service.Filter(parameter));
        }
        [HttpGet("get-by-role")]
        public async Task<ActionResult<IEnumerable<ReportTypeDto>>> GetByRole([FromQuery] int roleId)
        {
            return Ok(await _service.GetByRole(roleId));
        }


        [HttpPut("active/{id}")]
        public async Task<ActionResult<CusResponse>> Active(int id, [FromBody] ReportTypeDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            else
            {
                return Ok(await _service.Active(dto));
            }
        }
        [HttpPut("inactive/{id}")]
        public async Task<ActionResult<CusResponse>> Inactive(int id, [FromBody] ReportTypeDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            else
            {
                return Ok(await _service.Inactive(dto));
            }
        }
    }
}
