using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Malzamaty.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Malzamaty.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly TheContext _dbContext;
        public ReportController(TheContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllReports()
        {
            var result = await _dbContext.Report.Select(x => new { x.R_Description, x.F_ID }).ToListAsync();
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetReportsFromStudent(string F_ID)
        {
            var result = await _dbContext.Report.Where(x => x.F_ID == F_ID).Select(x => new { x.R_Description, x.F_ID }).ToListAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddReport([FromBody]Report report)
        {
           Report rep = new Report
            {
                R_Description = report.R_Description,
                R_Date=DateTime.Now,
                F_ID=report.F_ID
            };
            _dbContext.Report.Add(rep);
            await _dbContext.SaveChangesAsync();
            var St_ID = (from R in _dbContext.Report
                         join F in _dbContext.File
                         on R.F_ID equals F.F_ID
                         select F.Student.St_ID).Take(1);
                         return Ok(new { report,St_ID });
        }
        [HttpPut]
        public async Task<IActionResult> UpdateReport([FromBody]Report report, string R_ID)
        {
            var Rep = _dbContext.Report.Find(R_ID);
            Rep.R_Description = report.R_Description;
            _dbContext.Entry(Rep).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteReport(string R_ID)
        {
            var report = new Report() { R_ID = R_ID };
            _dbContext.Entry(report).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}