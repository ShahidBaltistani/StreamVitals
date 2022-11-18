using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vu360Sol.Service.Visitors;
using Vu360Sol.ViewModel.SharedViewModels;
using Vu360Sol.ViewModel.Visitors;

namespace VU360Sol.API.Controllers.Visitors
{
    [Route("visitor")]
    [ApiController]
    [Authorize]
    public class VisitorController : ControllerBase
    {
        private readonly VisitorService _service;
        public VisitorController(VisitorService service)
        {
            _service = service;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] VisitorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = await _service.AddVisitor(model);
                    return Ok(data);
                }
                catch (Exception)
                {

                    return BadRequest();
                }
            }
            return BadRequest("ModelState is Invalid");
        }
        [HttpPost("GetAllVisitorForLearning")]
        public async Task<IActionResult> GetAllVisitorForLearning([FromBody] string JsonObj)
        {
            var obj = JObject.Parse(JsonObj);
            var Search = obj["Search"].ToString();
            var PageNo = Convert.ToInt32(obj["PageNo"].ToString());
            var PageSize = Convert.ToInt32(obj["PageSize"].ToString());


            var doctors = await _service.GetAllVisitorForLearning(PageSize, PageNo, Search);
            var count = await _service.GetAllPageCountForLearning(Search);
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


            var doctors = await _service.GetAllVisitorForStarting(PageSize, PageNo, Search);
            var count = await _service.GetAllPageCountForStarting(Search);
            var page = new VisitorViewModelPaginationModel();
            page.visitorViewModels = doctors;
            page.Count = count;
            return Ok(page);
        }
    }
}
