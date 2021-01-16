using Malzamaty.Dto;
using Malzamaty.Model;
using Malzamaty.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Repository
{
    public class ReportRepository : BaseRepository<Report>, IReportRepository
    {
        private readonly TheContext _db;
        public ReportRepository(TheContext context) : base(context)
        {
            _db = context;
        }
        public async Task<Report> GetById(Guid id)=>
             await _db.Report.Include(x => x.File).FirstOrDefaultAsync(x => x.ID == id);
        public async Task<List<Report>> GetAll(int PageNumber, int count)
        {
            var Report = await _db.Report.Include(x => x.File).Skip((PageNumber - 1) * count).Take(count).ToListAsync();
            return Report;
        }
    }
}
