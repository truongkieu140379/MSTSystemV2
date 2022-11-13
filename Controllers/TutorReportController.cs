using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Dtos.ExtendedDtos;
using TutorSearchSystem.Filters;
using TutorSearchSystem.Global;
using TutorSearchSystem.Services.IService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TutorSearchSystem.Controllers
{
    [Authorize]
    [Route("api/tutor-report")]
    [ApiController]
    public class TutorReportController : ControllerBase
    {
        private readonly ITutorReportService _service;
        public TutorReportController(ITutorReportService service)
        {
            _service = service;
        }
        // GET: api/<TutorReportController>
        [HttpGet("all")]
        public async Task<IEnumerable<TutorReportDto>> Get()
        {
            return await _service.GetAll();
        }

        // GET api/<TutorReportController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TutorReportDto>> Get(int id)
        {
            var entity = await _service.GetById(id);
            if(entity != null)
            {
                return Ok(entity);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("count-pending")]
        public async Task<ActionResult<int>> CountPending()
        {
            return Ok(await _service.CountPending());
        }

        // POST api/<TutorReportController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TutorReportDto dto)
        {
            try
            {
                await _service.Insert(dto);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        
        [HttpPut("accept/{id}")]
        public async Task<ActionResult<CusResponse>> Accept(int id, [FromBody] TutorReportDto dto)
        {
            if(id != dto.Id)
            {
                return new CusResponse
                {
                    Status = false,
                    Type = "fail",
                    Message = "Id does not match"
                };
            }
            else
            {
                return Ok(await _service.Accept(dto));
            }
        }

        [HttpPut("deny/{id}")]
        public async Task<ActionResult<CusResponse>> Deny(int id, [FromBody] TutorReportDto dto)
        {
            if (id != dto.Id)
            {
                return new CusResponse
                {
                    Status = false,
                    Type = "fail",
                    Message = "Id does not match"
                };
            }
            else
            {
                return Ok(await _service.Deny(dto));
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult<Response<ExtendedTutorReportDto>>> Filter([FromQuery] TutorReportParameter parameter)
        {
            return Ok(await _service.Filter(parameter));
        }
    }
}
