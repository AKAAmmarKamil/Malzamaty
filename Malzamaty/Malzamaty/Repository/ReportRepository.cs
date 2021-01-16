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
        public async Task<Report> FindById(Guid id)
        { 
            var Result=await _db.Report.Include(x => x.File).FirstOrDefaultAsync(x => x.ID == id);
            if (Result == null) return null;
            return Result;
        }
        public async Task<IEnumerable<Report>> FindAll(int PageNumber, int count)=>await _db.Report.Include(x => x.File).Skip((PageNumber - 1) * count).Take(count).ToListAsync();
        
    }
}
