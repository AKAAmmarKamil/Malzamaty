using AutoMapper;
using Malzamaty.Model;
using Malzamaty.Repositories;
using System;

namespace Malzamaty.Services
{
    public interface IReportRepository : IBaseRepository<Report>
    {
    }
    public class ReportRepository : BaseRepository<Report>, IReportRepository
    {
        private readonly MalzamatyContext _db;
        public ReportRepository(MalzamatyContext context, IMapper mapper) : base(context, mapper)
        {
            _db = context;
        }
    }
}
