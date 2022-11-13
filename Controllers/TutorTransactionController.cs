using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TutorSearchSystem.Dtos;
using TutorSearchSystem.Dtos.ExtendedDtos;
using TutorSearchSystem.Services.IService;

namespace TutorSearchSystem.Controllers
{
    [Authorize]
    [Route("api/tutor-transactions")]
    [ApiController]
    public class TutorTransactionController : ControllerBase
    {
        private readonly ITutorTransactionService _service;

        public TutorTransactionController(ITutorTransactionService service)
        {
            _service = service;
        }

        [HttpGet("tutor/{tutorId}")]
        public async Task<ActionResult<IEnumerable<ExtendedTutorTransactionDto>>> GetTransactionByTutor(int tutorId)
        {
            try
            {
                var courses = await _service.GetTransactionByTutor(tutorId);
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

        [HttpPost]
        public async Task<ActionResult<TutorTransactionDto>> Post(TutorTransactionDto dto)
        {
            await _service.PostTutorTransaction(dto);
            return NoContent();
        }

        [HttpGet("get-total-amount-in-month")]
        public async Task<ActionResult<float>> GetTotalAmountInMonth()
        {
            float amount = await _service.GetTotalAmountInMonth();
            return Ok(amount);
        }

        [HttpGet("get-total-amount-per-month")]
        public async Task<ActionResult<IEnumerable<ReportTransactionDto>>> GetSumAmount(int year)
        {
            try{
                var result = await _service.GetSumAmount(year);
                if (result.Any())
                {
                    return Ok(result);
                }
                else
                {
                    return NoContent();
                }
            }catch(Exception){
                throw;
            }
        }
    }

}
