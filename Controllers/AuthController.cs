using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TutorSearchSystem.Global;
using TutorSearchSystem.Services.IService;
using TutorSearchSystem.TokenSettings;

namespace TutorSearchSystem.Controllers
{
   
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }


        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Duong Chinh Ngu", "Mary Mai" };
        }

      
        [HttpPost("authenticate")]
        public async Task<ActionResult<TSTokenSetting>> Authenticate(string email)
        {
            TSTokenSetting token = await _service.Authenticate(email);

            if (token == null)
            {
                return NotFound();
            }
                
            return Ok(token);
        }
    }
}
