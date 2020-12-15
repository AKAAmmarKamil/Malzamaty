using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Malzamaty.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Malzamaty.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly TheContext _dbContext;
        public StudentController(TheContext dbContext)
        {
            _dbContext = dbContext;
        }/*
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
           // var result = await _dbContext.Users.Select(x => new { x.FullName, x.Email,x.Authentication,x.Class.C_Name, x.Subject.Su_Name }).ToListAsync();
            return Ok(/*result);
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody]User User)
        {
            /*var Exist = _dbContext.Exist.Where(x => x.C_ID ==User.C_ID && x.Su_ID ==User.Su_ID).Select(x => x.E_ID).FirstOrDefault();
            if (Exist!=null)
            {
                User Usr = new User
                {
                    FullName = User.FullName,
                    Email = User.Email,
                    Password = User.Password,
                    Authentication = User.Authentication,
                    C_ID = User.C_ID,
                    Su_ID = User.Su_ID
                };
                _dbContext.Users.Add(Usr);
                await _dbContext.SaveChangesAsync();
                return Ok(User);
            }
           // else return BadRequest("هذه المادة ليست متاحة");
            }
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