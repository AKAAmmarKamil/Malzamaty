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
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var result = await _dbContext.Student.Select(x => new { x.St_FullName, x.St_Email,x.St_Authentication,x.Class.C_Name, x.Subject.Su_Name }).ToListAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody]Student Student)
        {
            var Exist = _dbContext.Exist.Where(x => x.C_ID ==Student.C_ID && x.Su_ID ==Student.Su_ID).Select(x => x.E_ID).FirstOrDefault();
            if (Exist!=null)
            {
                Student Std = new Student
                {
                    St_FullName = Student.St_FullName,
                    St_Email = Student.St_Email,
                    St_Password = Student.St_Password,
                    St_Authentication = Student.St_Authentication,
                    C_ID = Student.C_ID,
                    Su_ID = Student.Su_ID
                };
                _dbContext.Student.Add(Std);
                await _dbContext.SaveChangesAsync();
                return Ok(Student);
            }
            else return BadRequest("هذه المادة ليست متاحة");
            }
        [HttpPut]
        public async Task<IActionResult> UpdateStudent([FromBody]Student student, string St_ID)
        {
            var std = _dbContext.Student.Find(St_ID);
            std.St_FullName = student.St_FullName;
            std.St_Email = student.St_Email;
            std.St_Password = student.St_Password;
            std.St_Authentication = student.St_Authentication;
            std.C_ID = student.C_ID;
            std.Su_ID = student.Su_ID;
            _dbContext.Entry(std).State = EntityState.Modified;
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
        public async Task<IActionResult> DeleteStudent(string St_ID)
        {
            var student = new Student() { St_ID = St_ID };
            _dbContext.Entry(student).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}