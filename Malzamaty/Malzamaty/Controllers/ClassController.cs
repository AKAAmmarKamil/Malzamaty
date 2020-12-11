using System;
using System.Linq;
using System.Threading.Tasks;
using Malzamaty.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Malzamaty.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly TheContext _dbContext;
        public ClassController(TheContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllClasses()
        {
            var result = await _dbContext.Class.Select(x => new { x.C_Name, x.C_Stage, x.C_Type, x.Country.Co_Name }).ToListAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddClass([FromBody]Class Class)
        {
            Class Cls = new Class
            {
                C_Name = Class.C_Name,
                C_Stage = Class.C_Stage,
                C_Type = Class.C_Type,
                Co_ID=Class.Co_ID
            };
            _dbContext.Class.Add(Cls);
          await _dbContext.SaveChangesAsync();
            return Ok(Class);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateClass([FromBody]Class TheClass, string C_ID)
        {
           var Cls = _dbContext.Class.Find(C_ID);
            Cls.C_Name = TheClass.C_Name;
            Cls.C_Stage = TheClass.C_Stage;
            Cls.C_Type = TheClass.C_Type;
            Cls.Co_ID = TheClass.Co_ID;
           _dbContext.Entry(Cls).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        public async Task< IActionResult> DeleteClass(string C_ID)
        {
            var TheClass = new Class { C_ID = C_ID };
            _dbContext.Entry(TheClass).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}