using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Malzamaty.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly IMapper _mapper;
        public UserController(IRepositoryWrapper wrapper, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<UserReadDto>> GetUserById(Guid Id)
        {
            var User = await _wrapper.User.FindById(Id);
            if (User == null)
            {
                return NotFound();
            }
            var Interests = _wrapper.Interest.GetInterests(Id);
            var InterestsList = new List<List<string>>();
            var InterestsList2 = new List<string>();

            //InterestsList2.Add(Interests);
            var UserModel = new UserReadDto();
            UserModel.ID = User.ID;
            UserModel.UserName = User.UserName;
            UserModel.Email = User.Email;
            UserModel.Roles = User.Roles.Role;
            UserModel.Interests = null;
            return Ok(UserModel);
        }
        [HttpGet("{PageNumber}/{Count}")]
        public async Task<ActionResult<UserReadDto>> GetAllUsers(int PageNumber, int Count)
        {
            var Users = _wrapper.User.FindAll(PageNumber, Count);
            var UserModel= _mapper.Map<UserReadDto>( Users);
            return Ok(UserModel);
        }
        [HttpPost]
        public async Task<ActionResult<UserReadDto>> AddUser([FromBody]UserWriteDto UserWriteDto)
        {
            var Check = false;
            var Role = _wrapper.User.GetRole(UserWriteDto.Authentication);
            if (Role == "Teacher")
            {
                for (int i = 0; i < UserWriteDto.Interests.Count; i++)
                {
                    Check = _wrapper.User.Match(UserWriteDto.Interests[i].C_ID, UserWriteDto.Interests[i].Su_ID);
                    if (Check == false)
                    {
                        return BadRequest("المادة والصف غير متوافقان");
                    }
                }
            }
            else if (Role == "Student")
            {
                
                Check = _wrapper.User.Match(UserWriteDto.Interests[0].C_ID, UserWriteDto.Interests[0].Su_ID);
                if (Check == false)
                {
                    return BadRequest("المادة والصف غير متوافقان");
                }
            }
            var UserModel = _mapper.Map<User>(UserWriteDto);
            await _wrapper.User.Create(UserModel);
            var Interest = new InterestWriteDto();
            var InterestModel = new Interests();
            for (int i = 0; i < UserWriteDto.Interests.Count; i++)
            {
                Interest.User = UserModel.ID;
                Interest.Class = UserWriteDto.Interests[i].C_ID;
                Interest.Subject = UserWriteDto.Interests[i].Su_ID;
                InterestModel = _mapper.Map<Interests>(Interest);
                await _wrapper.Interest.Create(InterestModel);
            }
            var UserReadDto = _mapper.Map<UserReadDto>(UserModel);
            return Ok();

            
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid Id, [FromBody] UserUpdateDto UserUpdateDto)
        {
            var UserModelFromRepo = _wrapper.User.FindById(Id);
            if (UserModelFromRepo.Result == null)
            {
                return NotFound();
            }
            UserModelFromRepo.Result.UserName = UserUpdateDto.UserName;
            UserModelFromRepo.Result.Email = UserUpdateDto.Email;
            _wrapper.User.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid Id)
        {
            var User = _wrapper.User.Delete(Id);
            if (User.Result == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}