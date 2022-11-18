using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vu360Sol.Service.Practices;
using Vu360Sol.ViewModel.Practices;
using Vu360Sol.ViewModel.SharedViewModels;

namespace VU360Sol.API.Controllers.Practices
{
    [Route("Practice")]
    [ApiController]
    [Authorize]
    public class PracticeController : ControllerBase
    {
        private readonly PracticesSercice _service;
        public PracticeController(PracticesSercice service)
        {
            _service = service;
        }
        [HttpGet("getallPractice")]
        public async Task<IActionResult> GetAllPractice()
        {
            var doctors = await _service.GetAllPractice();
            if (doctors.Count() == 0)
                return NotFound("No data found");

            return Ok(doctors);
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
        public async Task<IActionResult> Create([FromBody] PracticeViewModel model)
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

        [HttpPost("Edit")]
        public async Task<IActionResult> Update([FromBody] PracticeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = await _service.Update(model);
                    if (data != null)
                    {
                        return Ok("Succeed");
                    }
                    return NotFound("Something went wrong please try again");
                }
                catch (Exception)
                {

                    return BadRequest("Something went wrong please try again");
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
        [HttpPost("getall")]
        public async Task<IActionResult> GetAll([FromBody] string JsonObj)
        {
            var obj = JObject.Parse(JsonObj);
            var Search = obj["Search"].ToString();
            var PageNo = Convert.ToInt32(obj["PageNo"].ToString());
            var PageSize = Convert.ToInt32(obj["PageSize"].ToString());

            var doctors = await _service.GetAll(PageSize, PageNo, Search);
            var count = await _service.GetAllPageCount(Search);
            var page = new PracticeViewModelPaginationModel();
            page.practice = doctors;
            page.Count = count;
            return Ok(page);
        }
    }
}
