using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vu360Sol.Service.Emails;
using Vu360Sol.ViewModel.Emails;

namespace VU360Sol.API.Controllers.Emails
{
    [Route("email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly SentEmailService _service;
        public EmailController(SentEmailService service)
        {
            _service = service;
        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var doctors = await _service.GetAll();
            if (doctors.Count() == 0)
                return NotFound("No data found");

            return Ok(doctors);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var doctor = await _service.Get(id);
            if (doctor == null)
                return NotFound("No data found");

            return Ok(doctor);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SentEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = await _service.Add(model);
                    return Ok(data);
                }
                catch (Exception)
                {

                    return BadRequest();
                }
            }
            return BadRequest("ModelState is Invalid");
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SentEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = await _service.Update(model);
                    return Ok(data);
                }
                catch (Exception)
                {

                    return BadRequest();
                }
            }
            return BadRequest("ModelState is Invalid");
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a Valid Id");

            var data = await _service.Delete(id);
            return Ok("Deleted");
        }
    }
}
