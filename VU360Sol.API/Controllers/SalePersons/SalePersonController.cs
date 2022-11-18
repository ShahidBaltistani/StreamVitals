using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Vu360Sol.Service.SalePersons;
using Vu360Sol.ViewModel.SalePersons;
using Vu360Sol.ViewModel.SharedViewModels;

namespace VU360Sol.API.Controllers.SalePersons
{
    [Route("saleperson")]
    [ApiController]
    [Authorize]
    public class SearchingController : ControllerBase
    {
        private readonly SalePersonService _service;
        public SearchingController(SalePersonService service)
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
            var page = new SalePersonViewModelPaginationModel();
            page.salePersonViewModels = doctors;
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
            var page = new SalePersonViewModelPaginationModel();
            page.salePersonViewModels = doctors;
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
        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SalePersonViewModel model)
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
        public async Task<IActionResult> Update([FromBody] SalePersonViewModel model)
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
       

       
        
        [HttpGet("GetDoctorAssignedList")]
        public async Task<IActionResult> GetDoctorAssignedList()
        {
            var data = await _service.GetDoctorAssignedList();
            return Ok(data);
        }
        [HttpGet("getallgender")]
        public async Task<IActionResult> GetAllGender()
        {
            var genders = await _service.GetAllGender();
            if (genders.Count() == 0)
                return NotFound("No data found");

            return Ok(genders);
        }
        [HttpGet("SalePersonApproval/{Id}")]
        public async Task<IActionResult> SalePersonApproval(int Id)
        {
            var data = await _service.SalePersonApproval(Id);
            if (data == null)
                return NotFound("No data found");

            return Ok(data);
        }
    }
}
