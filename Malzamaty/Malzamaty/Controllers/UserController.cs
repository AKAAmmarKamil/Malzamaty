using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Form;
using Malzamaty.Model;
using Malzamaty.Model.Form;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestSharp;

namespace Malzamaty.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;
        public UserController(IRepositoryWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginForm form)
        {
            var user = await _wrapper.User.Authintication(form);
            if (user != null)
            {  
                var claims = new[]
                {
                   new Claim("ID", user.ID.ToString()),
                   new Claim("Username", user.UserName),
                   new Claim("Email", user.Email),
                   new Claim("Activated", user.Activated.ToString()),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("Role", user.Role),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddDays(30),
                  notBefore: DateTime.UtcNow, audience: "Audience", issuer: "Issuer",
                  signingCredentials: new SigningCredentials(
                      new SymmetricSecurityKey(
                          Encoding.UTF8.GetBytes("Hlkjds0-324mf34pojf-14r34fwlknef0943")),
                      SecurityAlgorithms.HmacSha256));
                var Token = new JwtSecurityTokenHandler().WriteToken(token);
                var expire = DateTime.UtcNow.AddDays(30);
                return Ok(new { Token = Token, Expire = expire });

            }
            else return BadRequest();

        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword([FromBody] EmailForm EmailForm)
        {
            var Code=SendEmail.SendMessage(EmailForm.Email);
            var User = _wrapper.User.GetUserByEmail(EmailForm.Email);
            if (User.Result == null)
                return BadRequest(new { ERROR = "لم يتم العثور على حسابك" });
                User.Result.Activated = false;
                _wrapper.User.SaveChanges();
            if (Code != null)
            {
                var claims = new[]
                {
                   new Claim("Email", EmailForm.Email),
                   new Claim("Code", Code.ToString()),
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddMinutes(5),
                  notBefore: DateTime.UtcNow, audience: "Audience", issuer: "Issuer",
                  signingCredentials: new SigningCredentials(
                      new SymmetricSecurityKey(
                          Encoding.UTF8.GetBytes("Hlkjds0-324mf34pojf-14r34fwlknef0943")),
                      SecurityAlgorithms.HmacSha256));
                var Token = new JwtSecurityTokenHandler().WriteToken(token);
                var expire = DateTime.UtcNow.AddMinutes(1);
                return Ok(new { Token = Token, Expire = expire });

            }
            else return BadRequest();
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<UserReadDto>> GetUserById(Guid Id)
        {
            var User = await _wrapper.User.FindById(Id);
            var Interest = await _wrapper.Interest.GetInterests(Id);
            var InterestModel= _mapper.Map<List<InterestReadDto>>(Interest);
            if (User == null)
            {
                return NotFound();
            }
            var UserModel = _mapper.Map<UserReadDto>(User);
            UserModel.Interests = InterestModel;
            return Ok(UserModel);
        }
        [HttpGet("{PageNumber}/{Count}")]
        public async Task<ActionResult<UserReadDto>> GetAllUsers(int PageNumber, int Count)
        {
            var Users = _wrapper.User.FindAll(PageNumber, Count).Result.ToList();
            var Interest = new List<Interests>();
            var InterestModel = new List<InterestReadDto>();
            var UserModel= _mapper.Map<List<UserReadDto>>(Users);
            for (int i = 0; i < Users.Count(); i++)
            {
                Interest = await _wrapper.Interest.GetInterests(Users[i].ID);
                InterestModel = _mapper.Map<List<InterestReadDto>>(Interest);
                UserModel[i].Interests = InterestModel;
            }
             return Ok(UserModel);
        }
        [HttpPost]
        public async Task<ActionResult<UserReadDto>> AddUser([FromBody]UserWriteDto UserWriteDto)
        {
            var UserModel = _mapper.Map<User>(UserWriteDto);
            await _wrapper.User.Create(UserModel);
            var Interest = new InterestWriteDto();
            var InterestModel = new Interests();
            for (int i = 0; i < UserWriteDto.Interests.Count; i++)
            {
                Interest.Class = UserWriteDto.Interests[i].ClassID;
                Interest.Subject = UserWriteDto.Interests[i].SubjectID;
                InterestModel = _mapper.Map<Interests>(Interest);
                await _wrapper.Interest.Create(InterestModel);
            }

            var UserReadDto = _mapper.Map<UserReadDto>(UserModel);
            return Ok(UserReadDto);
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordForm ChangePasswordForm)
        {
            var Email = GetClaim("Email");
            var User =await _wrapper.User.GetUserByEmail(Email);
            if (User.Activated == true)
            {
                var UserModelFromRepo = await _wrapper.User.GetUserByEmail(Email);
                if (UserModelFromRepo == null)
                {
                    return NotFound();
                }
                UserModelFromRepo.Password = ChangePasswordForm.Password;
                _wrapper.User.SaveChanges();
                return NoContent();
            }
            return BadRequest(new { Message = "الرمز غير صحيح" });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid Id, [FromBody] UserUpdateDto UserUpdateDto)
        {
            var UserModelFromRepo =await _wrapper.User.FindById(Id);
            if (UserModelFromRepo == null)
            {
                return NotFound();
            }
            UserModelFromRepo.UserName = UserUpdateDto.UserName;
            UserModelFromRepo.Email = UserUpdateDto.Email;
            _wrapper.User.SaveChanges();
            return NoContent();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SendCode([FromBody] CodeForm CodeForm )
        {
            var Code= GetClaim("Code");
            var Email = GetClaim("Email");
            var User = _wrapper.User.GetUserByEmail(Email);
            if (CodeForm.Code.ToString() == Code)
            {
                User.Result.Activated = true;
                _wrapper.User.SaveChanges();
                return Ok(new {Message="تم تفعيل حساب المستخدم" });
            }
            return BadRequest(new { Message = "الرمز غير صحيح" });
        }
       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid Id)
        {
            var User =await _wrapper.User.Delete(Id);
            if (User == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}