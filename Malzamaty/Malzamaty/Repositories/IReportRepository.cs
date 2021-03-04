using AutoMapper;
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
        Task<List<Report>> GetReportsByFileId(Guid Id);
    }
    public class ReportRepository : BaseRepository<Report>, IReportRepository
    {
        private readonly MalzamatyContext _db;
        public ReportRepository(MalzamatyContext context, IMapper mapper) : base(context, mapper)
        {
            _db = context;
        }

        public async Task<List<Report>> GetReportsByFileId(Guid Id)=>await _db.Report.Where(x => x.File.ID == Id).ToListAsync();
    }
}
