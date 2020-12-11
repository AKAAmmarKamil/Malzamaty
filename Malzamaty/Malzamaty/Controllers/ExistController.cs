using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Malzamaty.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Malzamaty.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class ExistController : ControllerBase
    {
        private readonly TheContext _dbContext;
        public ExistController(TheContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllExistes()
        {
            var result = await _dbContext.Exist.Select(x => new { x.Class.C_Name, x.Subject.Su_Name }).ToListAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddExist([FromBody]Exist exist)
        {
            Exist Exi = new Exist
            {
                E_ID = exist.E_ID,
                C_ID = exist.C_ID,
                Su_ID = exist.Su_ID
            };
            _dbContext.Exist.Add(Exi);
            await _dbContext.SaveChangesAsync();
            return Ok(exist);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateExist([FromBody]Exist exist, string E_ID)
        {
            var Exi = _dbContext.Exist.Find(E_ID);
            Exi.C_ID = exist.C_ID;
            Exi.Su_ID = exist.Su_ID;
            _dbContext.Entry(Exi).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteExist(string E_ID)
        {
            var Exist = new Exist { E_ID = E_ID };
            _dbContext.Entry(Exist).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}