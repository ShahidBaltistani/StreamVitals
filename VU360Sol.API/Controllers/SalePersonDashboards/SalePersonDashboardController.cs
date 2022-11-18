using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vu360Sol.Service.SalePersonDashboards;
using Vu360Sol.ViewModel.SharedViewModels;

namespace VU360Sol.API.Controllers.SalePersonDashboards
{
    [Route("SalePersonDashboard")]
    [ApiController]
    [Authorize]
    public class SalePersonDashboardController : ControllerBase
    {
        private readonly SalePersonDashboardService _service;
        public SalePersonDashboardController(SalePersonDashboardService service)
        {
            _service = service;
        }


        ///ContactedDoctors Section
  

        [HttpPost("ContactedDoctors")]
        public async Task<IActionResult> ContactedDoctors([FromBody] string JsonObj)
        
        {
            var obj = JObject.Parse(JsonObj);
            var Search = obj["Search"].ToString();
            var PageNo = Convert.ToInt32(obj["PageNo"].ToString());
            var PageSize = Convert.ToInt32(obj["PageSize"].ToString());
            var Days = Convert.ToInt32(obj["Days"].ToString());
            var SalePersonId = Convert.ToInt32(obj["SalePersonId"].ToString());


            var data = await _service.ContactedDoctors(PageSize, PageNo, Search, Days, SalePersonId);
            var count = await _service.CountContactedDoctors(Search, Days, SalePersonId);
            var page = new DoctorPaginationModel();
            page.doctors = data;
            page.Count = count;
            return Ok(page);
        }

        [HttpPost("DoctorAssignedList")]
        public async Task<IActionResult> DoctorAssignedList([FromBody] string JsonObj)
        {
            var obj = JObject.Parse(JsonObj);
            var Search = obj["Search"].ToString();
            var PageNo = Convert.ToInt32(obj["PageNo"].ToString());
            var PageSize = Convert.ToInt32(obj["PageSize"].ToString());
            int Days = Convert.ToInt32(obj["Days"].ToString());
            var SalePersonId = Convert.ToInt32(obj["SalePersonId"].ToString());



            var doctors = await _service.DoctorAssignedList(PageSize, PageNo, Search, Days, SalePersonId);
            var count = await _service.CountDoctorAssignedList(Search, Days, SalePersonId);
            var page = new DoctorAssignedPaginationModel();
            page.doctorsAssigned = doctors;
            page.Count = count;
            return Ok(page);
        }


        ///Visitor Section

        [HttpPost("GetAllVisitorForLearning")]
        public async Task<IActionResult> GetAllVisitorForLearning([FromBody] string JsonObj)
        {
            var obj = JObject.Parse(JsonObj);
            var Search = obj["Search"].ToString();
            var PageNo = Convert.ToInt32(obj["PageNo"].ToString());
            var PageSize = Convert.ToInt32(obj["PageSize"].ToString());
            var Days = Convert.ToInt32(obj["Days"].ToString());


            var doctors = await _service.GetAllVisitorForLearning(PageSize, PageNo, Search, Days);
            var count = await _service.GetAllPageCountForLearning(Search, Days);
            var page = new VisitorViewModelPaginationModel();
            page.visitorViewModels = doctors;
            page.Count = count;
            return Ok(page);
        }
        [HttpPost("GetAllVisitorForStarting")]
        public async Task<IActionResult> GetAllVisitorForStarting([FromBody] string JsonObj)
        {
            var obj = JObject.Parse(JsonObj);
            var Search = obj["Search"].ToString();
            var PageNo = Convert.ToInt32(obj["PageNo"].ToString());
            var PageSize = Convert.ToInt32(obj["PageSize"].ToString());
            var Days = Convert.ToInt32(obj["Days"].ToString());


            var doctors = await _service.GetAllVisitorForStarting(PageSize, PageNo, Search, Days);
            var count = await _service.GetAllPageCountForStarting(Search, Days);
            var page = new VisitorViewModelPaginationModel();
            page.visitorViewModels = doctors;
            page.Count = count;
            return Ok(page);
        }

        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var data =await _service.GetById(Id);
            return Ok(data);
        }
    }
}
