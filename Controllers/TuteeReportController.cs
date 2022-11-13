﻿using System;
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
    [Route("api/tutee-report")]
    [ApiController]
    public class TuteeReportController : ControllerBase
    {
        private readonly ITuteeReportService _service;
        public TuteeReportController(ITuteeReportService service)
        {
            _service = service;
        }

        [HttpGet("filter")]
        public async Task<ActionResult<Response<ExtendedTuteeReportDto>>>Filter([FromQuery]TuteeReportParameter parameter)
        {
            return Ok(await _service.Filter(parameter));
        }
        [HttpGet("count-pending")]
        public async Task<ActionResult<int>> CountPending()
        {
            return Ok(await _service.CountPending());
        }

        [HttpPost]
        public async Task<IActionResult>Insert([FromBody] TuteeReportDto dto)
        {
            try
            {
                await _service.Insert(dto);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

        [HttpPut("accept/{id}")]
        public async Task<ActionResult<CusResponse>>Accept(int id , [FromBody] TuteeReportDto dto)
        {
            if(id != dto.Id)
            {
                return BadRequest();
            }
            else
            {
                return await _service.Accept(dto);
            }
        }
        [HttpPut("deny/{id}")]
        public async Task<ActionResult<CusResponse>> Deny( int id, [FromBody] TuteeReportDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            else
            {
                return await _service.Deny(dto);
            }
        }
    }
}
