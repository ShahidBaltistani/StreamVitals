using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Vu360Sol.Service.Doctors;
using Vu360Sol.Service.RequestDemoes;
using Vu360Sol.ViewModel.RequestDemoes;
using Vu360Sol.ViewModel.SharedViewModels;

namespace VU360Sol.API.Controllers.RequestDemoes
{
    [Route("requestdemo")]
    [ApiController]
    [Authorize]
    public class RequestDemoController : ControllerBase
    {
        private readonly RequestDemoService _service;
        private readonly DoctorService _doctorService;
        public RequestDemoController(RequestDemoService service,DoctorService doctorService)
        {
            _service = service;
            _doctorService = doctorService;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var doctors = await _service.GetAll();
            if (doctors.Count() == 0)
                return NotFound("No data found");

            return Ok(doctors);
        }

        [HttpPost("getall")]
        public async Task<IActionResult> GetAll([FromBody] string JsonObj)
        {
            var obj = JObject.Parse(JsonObj);
            var Search = obj["Search"].ToString();
            var PageNo = Convert.ToInt32(obj["PageNo"].ToString());
            var PageSize = Convert.ToInt32(obj["PageSize"].ToString());


            var data = await _service.GetAll(PageSize, PageNo, Search);
            var count = await _service.GetAllPageCount(Search);
            var page = new RequestDemoViewModelPaginationModel();
            page.requestDemoViewModels = data;
            page.Count = count;
            return Ok(page);
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
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] RequestDemoViewModel model)
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
        public async Task<IActionResult> Update([FromBody] RequestDemoViewModel model)
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

        [HttpPost("CreateDoctorRequest")]
        public async Task<IActionResult> CreateDoctorRequest([FromBody] RequestDemoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool AlreadyRegistered = false;
                    if (model.Doctor != null && model.Doctor.User != null)
                    {
                        AlreadyRegistered = model.Doctor.User.IsActive;
                        model.Doctor = null;
                    }
                        var data = await _service.Add(model);
                    if(!AlreadyRegistered && model.DoctorId >0 )
                    {
                        var doc = await _doctorService.UpdateDoctorStatus(Convert.ToInt32(model.DoctorId), 2);
                    }
                   
                    return Ok(data);
                }
                catch (Exception)
                {

                    return BadRequest();
                }
            }
            return BadRequest("ModelState is Invalid");
        }
        [HttpGet("getallbydoctorid/{UserId}")]
        public async Task<IActionResult> GetAllByDoctorId(int UserId)
        {
            var data = await _service.GetAllByDoctorId(UserId);
            if (data.Count() == 0)
                return NotFound("No data found");

            return Ok(data);
        }

    }
}
