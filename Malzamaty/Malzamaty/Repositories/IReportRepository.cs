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
        protected readonly Mapper _mapper;
        public ReportRepository(MalzamatyContext context, Mapper mapper) : base(context, mapper)
        {
            _db = context;
        }
    }
}
