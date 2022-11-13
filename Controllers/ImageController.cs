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
    [Route("api/images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _service;

        public ImageController(IImageService service)
        {
            _service = service;
        }

        // GET: api/<AccountController>
        [HttpGet("all")]
        public async Task<IEnumerable<ImageDto>> Get()
        {
            return await _service.GetAll();
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImageDto>> Get(int id)
        {
            ImageDto dto = await _service.GetById(id);
            if (dto == null)
            {
                return NotFound();
            }
            return dto;
        }

        // POST api/<AccountController>
        [HttpPost]
        public async Task<ActionResult<ImageDto>> Post(ImageDto dto)
        {
            await _service.Insert(dto);
            return NoContent();
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string email, ImageDto dto)
        {
            if (email != dto.OwnerEmail)
            {
                return BadRequest();
            }
            try
            {
                await _service.Update(dto);
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;

            }
            return NoContent();
        }


        ////delete image
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _service.GetById(id) == null)
            {
                return NotFound();
            }
            else
            {
                await _service.Delete(id);
            }
            return NoContent();
        }

        [HttpDelete("owner-email/{email}")]
        public async Task<IActionResult> DeleteByOwnerEmail(string email)
        {
            if (await _service.Get(email, "certification") == null)
            {
                return Ok(null);
            }
            else
            {
                await _service.DeleteByOwnerEmail(email);
            }
            return NoContent();
        }

        //get image by and ownerId
        [HttpGet("get/{result}")]
        public async Task<ActionResult<string[]>> Get(string ownerEmail, string imageType)
        {
            try
            {
                var dtos = await _service.Get(ownerEmail, imageType);
                if (dtos.Any())
                {
                    return Ok(dtos);
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
