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
using Vu360Sol.Service.Search;
using Vu360Sol.ViewModel.SalePersons;
using Vu360Sol.ViewModel.SharedViewModels;

namespace VU360Sol.API.Controllers.Search
{
    [Route("searching")]
    [ApiController]
    [Authorize]
    public class SearchingController : ControllerBase
    {
        private readonly SearchingService _service;
        public SearchingController(SearchingService service)
        {
            _service = service;
        }
        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] string JsonObj)
        {
            var obj = JObject.Parse(JsonObj);
            var Search = obj["Search"].ToString();
            var PageNo = Convert.ToInt32(obj["PageNo"].ToString());
            var PageSize = Convert.ToInt32(obj["PageSize"].ToString());


            var doctors = await _service.Search(PageSize, PageNo, Search);
            var count = await _service.GetAllPageCount(Search);
            var page = new SearchingPaginationModel();
            page.SearchingViewModels = doctors;
            page.Count = count;
            return Ok(page);
        }
    }
}
