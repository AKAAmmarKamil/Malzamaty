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
    public class RatingController : BaseController
    {
        private readonly MalzamatyContext _dbContext;
        public RatingController(MalzamatyContext dbContext)
        {
            _dbContext = dbContext;
        }/*
        [HttpGet]
        public async Task<IActionResult> GetAllRatings()
        {
            var result = await _dbContext.Rating.Where(x => x.Ra_Rate != 101).Select(x => new { x.Ra_Comment, x.Ra_Rate, x.Student.St_ID, x.F_ID }).ToListAsync();
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetRatingsFromFile(string F_ID)
        {
            var result = await _dbContext.Rating.Where(x => x.Ra_Rate != 101 && x.F_ID == F_ID).Select(x => new { x.Ra_Comment, x.Ra_Rate, x.Student.St_ID, x.F_ID }).ToListAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddRate([FromBody]Rating TheRating, string St_ID,string F_ID)
        {
            var con = new SqlConnection("Server=DESKTOP-A1QK3DR;Database=Malzamaty;Trusted_Connection=True;ConnectRetryCount=0");
            var cmd = new SqlCommand("RatingProcedure", con);
                    await con.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Ra_Comment", TheRating.Ra_Comment);
                    cmd.Parameters.AddWithValue("@Ra_Rate", TheRating.Ra_Rate);
                    cmd.Parameters.AddWithValue("@St_ID", St_ID);
                    cmd.Parameters.AddWithValue("@F_ID", F_ID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return Ok(dt);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRating([FromBody]Rating rating, string Su_ID)
        {
            var Rat = _dbContext.Rating.Find(Su_ID);
            Rat.Ra_Comment = rating.Ra_Comment;
            Rat.Ra_Rate = rating.Ra_Rate;
            _dbContext.Entry(Rat).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRating(string Ra_ID)
        {
            var rating = new Rating() { Ra_ID = Ra_ID };
            _dbContext.Entry(rating).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }*/
    }
}