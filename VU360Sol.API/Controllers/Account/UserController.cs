using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Vu360Sol.Service.Account;
using Vu360Sol.ViewModel;
using Vu360Sol.ViewModel.Account;
using Vu360Sol.ViewModel.SharedViewModels;
using Vu360Sol.ViewModel.Visitors;

namespace VU360Sol.API.Controllers.Account
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserService _service;
        public UserController(IConfiguration config, UserService service)
        {
            _config = config;
            _service = service;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserViewModel model)
        {
            model.Email = model.Email.ToLower();
            if (await _service.UsernameExist(model))
                return BadRequest("User already exist");
            await _service.Register(model);
            return Ok("Successfully registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserViewModel model)
        {
            var userRepo = await _service.Login(model.Email.ToLower(), model.Password.ToLower());

            if (userRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userRepo.Email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config
                     .GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var usertoken = tokenHandler.WriteToken(token);
            return Ok(new { userRepo, usertoken });

        }

        [HttpPost("addVisitor")]
        public async Task<IActionResult> AddVisitor([FromBody] VisitorViewModel model)
        {
           
            
             await _service.AddVisitor(model);
            return Ok("Successfully registered");
        }
        [HttpPost("Usernameexist")]
        public async Task<IActionResult> UsernameExist([FromBody] UserViewModel user)
        {
            if (await _service.UsernameExist(user))
                return BadRequest("Username is already exist");

            return Ok();
        }
        [HttpGet("getallUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var doctors = await _service.GetAllUsers();
            if (doctors.Count() == 0)
                return NotFound("No data found");

            return Ok(doctors);
        }
        [HttpGet("Approve/{Id}")]
        public async Task<IActionResult> Approve(int Id)
        {
            var data = await _service.Approve(Id);
            if (data == null)
                return NotFound("No data found");

            return Ok(data);
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            if (await _service.ChangePassword(model, model.NewPassword))
                return Ok("password has been change successfully");

            return BadRequest("Please Enter Valid Password");

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
            var page = new UserViewModelPaginationModel();
            page.userViewModels = doctors;
            page.Count = count;
            return Ok(page);
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> UsernameExist(ForgotPasswordViewModel model)
        {
            var data = await _service.ForgotPassword(model);
            if (data != null)
            {
                if (data.IsActive == true)
                {
                    return Ok(data);
                }
                else if (data.IsActive == false && data.RoleId == 2)
                {
                  var dr =  await _service.GetDoctorByUserId(data.Id);
                    if (dr != null)
                    {
                        if (dr.IsActive == true)
                        {
                            return NotFound("You have not Create User yet, Please contact our team for more details");
                        }
                    }
                   
                }
                return NotFound("Email Doesn't Exist, Please try Again");
            }
            return NotFound("Email Doesn't Exist, Please try Again");
        }
    }
}
