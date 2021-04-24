using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Form;
using Malzamaty.Model;
using Malzamaty.Model.Dto;
using Malzamaty.Model.Form;
using Malzamaty.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace Malzamaty.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IInterestService _interestService;
        private readonly IMapper _mapper;
        private readonly IAddressService _addressService;
        public UserController(IUserService userService, IInterestService interestService, IMapper mapper, IAddressService addressService)
        {
            _userService = userService;
            _interestService = interestService;
            _mapper = mapper;
            _addressService = addressService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginForm form)
        {
            var user = await _userService.Authintication(form);
            if (user == null)
                return BadRequest(new Authintication
                {
                    Token = null,
                    Error = "UserName or Password Incorrect"
                });
            bool validPassword = BCrypt.Net.BCrypt.Verify(form.Password, user.Password);

            if (!validPassword)
            {
                return BadRequest(new Authintication
                {
                    Token = null,
                    Error = "UserName or Password Incorrect"
                });
            }
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
        [HttpPost]
        public async Task<IActionResult> ForgetPassword([FromBody] EmailForm EmailForm)
        {
            var Code = SendEmail.SendMessage(EmailForm.Email);
            var User = await _userService.GetUserByEmail(EmailForm.Email);
            if (User == null)
                return BadRequest(new { ERROR = "لم يتم العثور على حسابك" });
            await _userService.ChangeStatus(User.ID, false);
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
        [HttpGet("{Id}", Name = "GetUserById")]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<ActionResult<UserReadDto>> GetUserById(Guid Id)
        {
            var User = await _userService.FindById(Id);
            var Interest = await _interestService.GetInterests(Id);
            var Address = await _addressService.FindById(User.AddressID.GetValueOrDefault());
            var InterestModel = _mapper.Map<List<InterestReadDto>>(Interest);
            if (User == null)
            {
                return NotFound();
            }
            User.Address = Address;
            var UserModel = _mapper.Map<UserReadDto>(User);
            UserModel.Interests = InterestModel;
            return Ok(UserModel);
        }
        [HttpGet("{PageNumber}/{Count}")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<ActionResult<UserReadDto>> GetAllUsers(int PageNumber, int Count)
        {
            var Users = _userService.FindAll(PageNumber, Count).Result.ToList();
            var Interest = new List<Interests>();
            var InterestModel = new List<InterestReadDto>();
            var Address = new Address();
            var AddressModel = new AddressReadDto();
            var UserModel = _mapper.Map<List<UserReadDto>>(Users);
            for (int i = 0; i < Users.Count(); i++)
            {
                    Interest = await _interestService.GetInterests(Users[i].ID);
                    InterestModel = _mapper.Map<List<InterestReadDto>>(Interest);
                    Address= await _addressService.FindById(Users[i].AddressID.GetValueOrDefault());
                    AddressModel = _mapper.Map<AddressReadDto>(Address);
                    UserModel[i].Interests = InterestModel;
                    UserModel[i].Address = AddressModel;
            }
            return Ok(UserModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserWriteDto UserWriteDto)
        {
            UserWriteDto.Password = BCrypt.Net.BCrypt.HashPassword(UserWriteDto.Password);
            var UserModel = _mapper.Map<User>(UserWriteDto);
            var User=await _userService.Create(UserModel);
            var UserReadDto = new UserReadDto();
            var UserAdminReadDto = _mapper.Map<UserAdminsReadDto>(UserModel);
            if (UserWriteDto.Role!= "Admin" && UserWriteDto.Role != "DeliveryRepresentative" && UserWriteDto.Role != "DeliveryAdmin")
            {
                var Address = await _addressService.FindById(User.AddressID.GetValueOrDefault());
                User.Address = Address;
                var InterestWriteDto = new InterestWriteDto();
                var InterestModel = new Interests();
                for (int i = 0; i < UserWriteDto.Interests.Count; i++)
                {
                    InterestWriteDto.Class = UserWriteDto.Interests[i].ClassID;
                    InterestWriteDto.Subject = UserWriteDto.Interests[i].SubjectID;
                    InterestModel = _mapper.Map<Interests>(InterestWriteDto);
                    InterestModel.UserID = User.ID;
                    await _interestService.Create(InterestModel);
                }
                var Interest = await _interestService.GetInterests(User.ID);
                UserReadDto = _mapper.Map<UserReadDto>(UserModel);
                UserReadDto.Interests = _mapper.Map<List<InterestReadDto>>(Interest);
                return CreatedAtRoute("GetUserById", new { Id = UserReadDto.ID }, UserReadDto);
            }
            return CreatedAtRoute("GetUserById", new { Id = UserAdminReadDto.ID }, UserAdminReadDto);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordForm ChangePasswordForm)
        {
            var Email = GetClaim("Email");
            var User = await _userService.GetUserByEmail(Email);
            if (User.Activated == true)
            {
                var UserModelFromRepo = await _userService.GetUserByEmail(Email);
                if (UserModelFromRepo == null)
                {
                    return NotFound();
                }
                ChangePasswordForm.Password = BCrypt.Net.BCrypt.HashPassword(ChangePasswordForm.Password);
                await _userService.ChangePassword(UserModelFromRepo.ID, ChangePasswordForm.Password);
                return NoContent();
            }
            return BadRequest(new { Message = "الرمز غير صحيح" });
        }
        [HttpPut("{id}")]
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Student + "," + UserRole.Teacher)]
        public async Task<IActionResult> UpdateUser(Guid Id, [FromBody] UserUpdateDto UserUpdateDto)
        {
            if (GetClaim("Role") != "Admin" && GetClaim("ID") != Id.ToString())
            {
                return BadRequest(new { Error = "لا يمكن تعديل بيانات تخص مستخدم آخر من دون صلاحية المدير" });
            }
            var UserModelFromRepo = await _userService.FindById(Id);
            if (UserModelFromRepo == null)
            {
                return NotFound();
            }
            var UserModel = _mapper.Map<User>(UserUpdateDto);
            await _userService.Modify(Id, UserModel);
            return NoContent();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendCode([FromBody] CodeForm CodeForm)
        {
            var Code = GetClaim("Code");
            var Email = GetClaim("Email");
            var User = await _userService.GetUserByEmail(Email);
            if (CodeForm.Code.ToString() == Code)
            {
                await _userService.ChangeStatus(User.ID, true);
                return Ok(new { Message = "تم تفعيل حساب المستخدم" });
            }
            return BadRequest(new { Message = "الرمز غير صحيح" });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> DeleteUser(Guid Id)
        {
            var User = await _userService.Delete(Id);
            if (User == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}