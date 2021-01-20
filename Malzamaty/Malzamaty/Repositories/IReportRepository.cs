using Malzamaty.Model;
using Malzamaty.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Services
{
    public interface IReportRepository : IBaseRepository<Report>
    {
    }
    public class ReportRepository : BaseRepository<Report>, IReportRepository
    {
        private readonly MalzamatyContext _db;
        public ReportRepository(MalzamatyContext context) : base(context)
        {
            _db = context;
        }
        public async Task<Report> FindById(Guid id)
        {
            var Result = await _db.Report.FirstOrDefaultAsync(x => x.ID == id);
            if (Result == null) return null;
            return Result;
        }
        public async Task<IEnumerable<Report>> FindAll(int PageNumber, int count) => await _db.Report.Skip((PageNumber - 1) * count).Take(count).ToListAsync();

    }
}
