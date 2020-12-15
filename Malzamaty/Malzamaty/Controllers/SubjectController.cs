using System.Linq;
using System.Threading.Tasks;
using Malzamaty.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Malzamaty.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly TheContext _dbContext;
        public SubjectController(TheContext dbContext)
        {
            _dbContext = dbContext;
        }/*
        [HttpGet]
        public async Task<IActionResult> GetAllSubject()
        {
            var result = await _dbContext.Subject.Select(x => x.Su_Name).ToListAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddSubject([FromBody]Subject subject)
        {
            Subject Sub = new Subject
            {
                Su_Name = subject.Su_Name
            };
            _dbContext.Subject.Add(Sub);
           await _dbContext.SaveChangesAsync();
            return Ok(subject);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSubject([FromBody]Subject subject, string Su_ID)
        {
            var Sub = _dbContext.Subject.Find(Su_ID);
            Sub.Su_Name = subject.Su_Name;
            _dbContext.Entry(Sub).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteSubjects(string Su_ID)
        {
            var subject = new Subject() { Su_ID = Su_ID };
            _dbContext.Entry(subject).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }*/
    }
}