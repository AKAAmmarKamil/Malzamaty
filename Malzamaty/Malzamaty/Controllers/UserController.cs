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
            var Subject = _wrapper.Subject.FindById(UserWriteDto.Interests[0].Su_ID);
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
            var Interest = new Interests();
            for (int i = 0; i < UserWriteDto.Interests.Count; i++)
            {
                Interest.U_ID = UserModel.ID;
                Interest.C_ID = UserWriteDto.Interests[i].C_ID;
                Interest.U_ID = UserWriteDto.Interests[i].Su_ID;
                Interest.User = new User();
                Interest.Class = new Class();
                Interest.Subject = new Subject();

                await _wrapper.Interest.Create(Interest);
            }
            var UserReadDto = _mapper.Map<UserReadDto>(UserModel);
            return Ok(UserReadDto);//CreatedAtRoute(nameof(GetUserById), new { Id = UserReadDto.Id }, UserReadDto);

            
        }
            /*
        [HttpPut]
        public async Task<IActionResult> UpdateStudent([FromBody]User user, string St_ID)
        {
            var usr = _dbContext.Users.Find(St_ID);
            usr.FullName = user.FullName;
            usr.Email = user.Email;
            usr.Password = user.Password;
            usr.Authentication = user.Authentication;
            usr.C_ID = user.C_ID;
            usr.Su_ID = user.Su_ID;
            _dbContext.Entry(usr).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateIntersted()
        {
            var con = new SqlConnection("Server=DESKTOP-A1QK3DR;Database=Malzamaty;Trusted_Connection=True;ConnectRetryCount=0");
            var cmd = new SqlCommand("UpdateInterstings", con);
            await con.OpenAsync();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return Ok(dt);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(string ID)
        {
            //var user = new User() { ID = ID };
            //_dbContext.Entry(User).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }*/
    }
}