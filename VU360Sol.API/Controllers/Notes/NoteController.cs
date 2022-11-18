using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vu360Sol.Service.Notes;
using Vu360Sol.ViewModel.Notes;

namespace VU360Sol.API.Controllers.Notes
{
    [Route("note")]
    [ApiController]
    [Authorize]
    public class NoteController : Controller
    {
        private readonly NoteService _service;
        public NoteController(NoteService service)
        {
            _service = service;
        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAll();
            if (data.Count() == 0)
                return NotFound("No data found");

            return Ok(data);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.Get(id);
            if (data == null)
                return NotFound("No data found");

            return Ok(data);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] NoteViewModel model)
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
        public async Task<IActionResult> Update([FromBody] NoteViewModel model)
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
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a Valid Id");

            var data = await _service.Delete(id);
            return Ok("Deleted");
        }
        [HttpGet("GetAllNotesByRequestDemoId/{RequestDemoId}")]
        public async Task<IActionResult> GetAllNotesByRequestDemoId(int RequestDemoId)
        {
            var data = await _service.GetAllNotesByRequestDemoId(RequestDemoId);
            if (data.Count() == 0)
                return NotFound("No data found");

            return Ok(data);
        }
    }
}
