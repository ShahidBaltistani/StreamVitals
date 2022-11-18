using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vu360Sol.Service.Dashboards;
using Vu360Sol.ViewModel.SharedViewModels;

namespace VU360Sol.API.Controllers.Dashboards
{
    [Route("Dashboard")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _service;
        public DashboardController(DashboardService service)
        {
            _service = service;
        }


        ///Approval Section
  

        [HttpPost("GetRequestDemo")]
        public async Task<IActionResult> GetRequestDemo([FromBody] string JsonObj)
        {
            var obj = JObject.Parse(JsonObj);
            var Search = obj["Search"].ToString();
            var PageNo = Convert.ToInt32(obj["PageNo"].ToString());
            var PageSize = Convert.ToInt32(obj["PageSize"].ToString());
            var Days = Convert.ToInt32(obj["Days"].ToString());


            var data = await _service.GetRquestDemo(PageSize, PageNo, Search, Days);
            var count = await _service.GetAllPageCountRequestDemo(Search, Days);
            var page = new RequestDemoViewModelPaginationModel();
            page.requestDemoViewModels = data;
            page.Count = count;
            return Ok(page);
        }

        [HttpPost("getallinactiveUsers")]
        public async Task<IActionResult> GetAllInActiveUser([FromBody] string JsonObj)
        {
            var obj = JObject.Parse(JsonObj);
            var Search = obj["Search"].ToString();
            var PageNo = Convert.ToInt32(obj["PageNo"].ToString());
            var PageSize = Convert.ToInt32(obj["PageSize"].ToString());
            int Days = Convert.ToInt32(obj["Days"].ToString());


            var doctors = await _service.GetAllInActive(PageSize, PageNo, Search, Days);
            var count = await _service.GetAllInActivePageCount(Search, Days);
            var page = new UserViewModelPaginationModel();
            page.userViewModels = doctors;
            page.Count = count;
            return Ok(page);
        }

        [HttpGet("Approve/{Id}")]
        public async Task<IActionResult> Approve(int Id)
        {
            var data = await _service.Approve(Id);
            if (data == null)
                return NotFound("No data found");

            return Ok(data);
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
    }
}
