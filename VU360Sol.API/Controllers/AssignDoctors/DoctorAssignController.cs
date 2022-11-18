using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Vu360Sol.Service.AssignDoctors;
using Vu360Sol.ViewModel.SalePersons;
using Vu360Sol.ViewModel.SharedViewModels;

namespace VU360Sol.API.Controllers.AssignDoctors
{
    [Route("DoctorAssign")]
    [ApiController]
    [Authorize]
    public class DoctorAssignController : ControllerBase
    {
        private readonly AssignDoctorService _service;
        public DoctorAssignController(AssignDoctorService service)
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
        [HttpPost("getallfordoctor")]
        public async Task<IActionResult> GetAllDoctorAssigned([FromBody] string JsonObj)
        {
            var obj = JObject.Parse(JsonObj);
            var Search = obj["Search"].ToString();
            var PageNo = Convert.ToInt32(obj["PageNo"].ToString());
            var PageSize = Convert.ToInt32(obj["PageSize"].ToString());


            var doctors = await _service.GetAllDoctorAssigned(PageSize, PageNo, Search);
            var count = await _service.GetAllPageCount(Search);
            var page = new DoctorAssignedViewModelPaginationModel();
            page.doctorAssignedViewModels = doctors;
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
        public async Task<IActionResult> Add(DoctorAssignedViewModel model)
        {
            //model.AssignedDate = Utility.getDefaultTime();
            var result = await _service.Add(model);
            return Ok(result);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a Valid Id");

            var data = await _service.Delete(id);
            return Ok("Deleted");
        }

        [HttpGet("GetByDoctorId/{id}")]
        public async Task<IActionResult> GetByDoctorId(int id)
        {
            var data = await _service.GetByDoctorId(id);
            return Ok(data);
        } 
       [HttpPost("GetBySalePersonIdPage")]
        public async Task<IActionResult> GetBySalePersonIdPage([FromBody] string JsonObj  )
        {
            
            var obj = JObject.Parse(JsonObj);
            var Search = obj["Search"].ToString();
            var PageNo = Convert.ToInt32(obj["PageNo"].ToString());
            var PageSize = Convert.ToInt32(obj["PageSize"].ToString());
            var SalePersonId = Convert.ToInt32(obj["SalePersonId"].ToString());
            var assigned = await _service.GetBySalePersonIdPage(PageSize,PageNo,Search,SalePersonId);
            var count = await _service.GetCountBySalePersonId( Search, SalePersonId);
            var page = new DoctorAssignedPaginationModel();
            page.doctorsAssigned = assigned;
            page.Count = count;
           
            return Ok(page);
        }

        [HttpPost("ContectedDoctorsPage")]
        public async Task<IActionResult> ContectedDoctorsPage([FromBody] string JsonObj)
        {
            var obj = JObject.Parse(JsonObj);
            var Search = obj["Search"].ToString();
            var PageNo = Convert.ToInt32(obj["PageNo"].ToString());
            var PageSize = Convert.ToInt32(obj["PageSize"].ToString());
            var UserId = Convert.ToInt32(obj["UserId"].ToString());
           

            var doctors = await _service.ContectedDoctorsPage(PageSize, PageNo, Search,UserId);
            var count = await _service.ContectedDoctorsCount(Search, UserId);
            var page = new DoctorPaginationModel();
            page.doctors = doctors;
            page.Count = count;

            return Ok(page);
        }
        [HttpGet("ContectedDoctorsReport/{NotesRequired}")]
        public async Task<IActionResult> ContectedDoctorsReport(bool NotesRequired = false)
        {
            var doctors = await _service.ContectedDoctorsReport(NotesRequired);
            //if (doctors.Count() == 0)
            //    return NotFound("No data found");

            return Ok(doctors);
        }

        [HttpGet("AllDoctorsReport/{NotesRequired}")]
        public async Task<IActionResult> AllDoctorsReport(bool NotesRequired = false)
        {
            var doctors = await _service.AllDoctorsReport(NotesRequired);
            //if (doctors.Count() == 0)
            //    return NotFound("No data found");

            return Ok(doctors);
        }

    }
}
