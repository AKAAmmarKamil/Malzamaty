using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Malzamaty.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Malzamaty.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly TheContext _dbContext;
        public ScheduleController(TheContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSchedules()
        {
            var result = await _dbContext.Schedules.Select(x => new { x.StartStudy, x.FinishStudy, x.Student.St_FullName, x.Subject.Su_Name }).ToListAsync();
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetSchedulesFromStudent(string St_ID)
        {
            var result = await _dbContext.Schedules.Where(x => x.Student.St_ID == St_ID).Select(x => new { x.StartStudy, x.FinishStudy, x.Student.St_FullName,x.Subject.Su_Name }).ToListAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Addschedule([FromBody]Schedule schedule,string St_ID,string Su_ID)
        {
            var con = new SqlConnection("Server=DESKTOP-A1QK3DR;Database=Malzamaty;Trusted_Connection=True;ConnectRetryCount=0");
            var cmd = new SqlCommand("AddSchedule", con);
            await con.OpenAsync();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StartStudy",schedule.StartStudy);
            cmd.Parameters.AddWithValue("@FinishStudy", schedule.FinishStudy);
            cmd.Parameters.AddWithValue("@St_ID", St_ID);
            cmd.Parameters.AddWithValue("@Su_ID", Su_ID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return Ok(dt);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSchedule([FromBody]Schedule schedule, string Su_ID)
        {
            var Sch = _dbContext.Schedules.Find(Su_ID);
            Sch.StartStudy = schedule.StartStudy;
            Sch.FinishStudy = schedule.FinishStudy;
            Sch.Student.St_ID = schedule.Student.St_ID;
            Sch.Subject.Su_ID = schedule.Subject.Su_ID;
            _dbContext.Entry(Sch).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteSchedules(string Sc_ID)
        {
            var Schedule = new Schedule() { Sc_ID = Sc_ID };
            _dbContext.Entry(Schedule).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}