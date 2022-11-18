using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vu360Sol.Service.EmailConfiguration;

namespace VU360Sol.API.Controllers.EmailConfiguration
{
    [Route("EmailConfiguration")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly CommonService _service;
        public CommonController(CommonService service)
        {
            _service = service;
        }
        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var Configuration = await _service.Get();
            if (Configuration == null)
                return NotFound("No data found");

            return Ok(Configuration);
        }
    }
}
