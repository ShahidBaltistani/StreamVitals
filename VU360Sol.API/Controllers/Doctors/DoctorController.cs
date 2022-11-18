using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Vu360Sol.Service.Doctors;
using Vu360Sol.Service.RequestDemoes;
using Vu360Sol.ViewModel.Doctors;
using Vu360Sol.ViewModel.SharedViewModels;

namespace VU360Sol.API.Controllers.Doctor
{
    [Route("doctor")]
    [ApiController]
    [Authorize]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorService _service;
        public DoctorController(DoctorService service)
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

        [HttpPost("getall")]
        public async Task<IActionResult> GetAll([FromBody] string JsonObj)
        {
            var obj = JObject.Parse(JsonObj);
            var Search = obj["Search"].ToString();
            var PageNo = Convert.ToInt32(obj["PageNo"].ToString());
            var PageSize = Convert.ToInt32(obj["PageSize"].ToString());


            var doctors = await _service.GetAll(PageSize, PageNo, Search);
            var count = await _service.GetAllPageCount(Search);
            var page = new DoctorViewModelPaginationModel();
            page.doctorViewModels = doctors;
            page.Count = count;
            return Ok(page);
        }
        [HttpPost("getallinactive")]
        public async Task<IActionResult> GetAllInActive([FromBody] string JsonObj)
        {
            var obj = JObject.Parse(JsonObj);
            var Search = obj["Search"].ToString();
            var PageNo = Convert.ToInt32(obj["PageNo"].ToString());
            var PageSize = Convert.ToInt32(obj["PageSize"].ToString());

            var doctors = await _service.GetAllInActive(PageSize, PageNo, Search);
            var count = await _service.GetAllInActivePageCount(Search);
            var page = new DoctorViewModelPaginationModel();
            page.doctorViewModels = doctors;
            page.Count = count;
            return Ok(page);
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
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] DoctorViewModel model)
{
            if (ModelState.IsValid)
            {
                try
                {
                    //var claimsIdentity = this.User.Identity as ClaimsIdentity;
                    //var currentUser = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;

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
        public async Task<IActionResult> Update([FromBody] DoctorViewModel model)
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
        [HttpGet("DoctorApproval/{Id}")]
        public async Task<IActionResult> DoctorApproval(int Id)
        {
            var data = await _service.DoctorApproval(Id);
            if (data == null)
                return NotFound("No data found");

            return Ok(data);
        }
        [HttpPost("ImportDoctors")]
        public async Task<IActionResult> ImportDoctors([FromBody] IEnumerable<DoctorViewModel> model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = await _service.ImportDoctors(model);
                    return Ok(data);
                }
                catch (Exception)
                {

                    return BadRequest();
                }
            }
            return BadRequest("ModelState is Invalid");
        }
    }
}
