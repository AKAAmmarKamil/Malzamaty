using Malzamaty.Model;
using Malzamaty.Repositories;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using AutoMapper;

namespace Malzamaty.Services
{
    public interface IScheduleRepository : IBaseRepository<Schedule>
    {
        Task<IEnumerable<Schedule>> GetUserSchedules(Guid Id);
        Task<IEnumerable<Schedule>> GetUserSchedules(int PageNumber, int Count, Guid Id);
        Task<bool> IsBewteenTwoDates(DateTimeOffset dt, DateTimeOffset start, DateTimeOffset end);
    }
    public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
    {
        private readonly MalzamatyContext _db;
        protected readonly IMapper _mapper;
        public ScheduleRepository(MalzamatyContext context, IMapper mapper) : base(context, mapper)
        {
            _db = context;
        }
        public async Task<bool> IsBewteenTwoDates(DateTimeOffset dt, DateTimeOffset start, DateTimeOffset end)
        { 
            return dt >= start && dt <= end;
        }
        public async Task<Schedule> FindById(Guid id)
        {
            var Result = await _db.Schedules.Include(x => x.Subject).FirstOrDefaultAsync(x => x.ID == id);

            if (Result == null) return null;
            return Result;
        }
        public async Task<IEnumerable<Schedule>> FindAll(int PageNumber, int Count)
        {
            return await _db.Schedules.Include(x => x.Subject).Skip((PageNumber - 1) * Count).Take(Count).ToListAsync();
        }
        public async Task<IEnumerable<Schedule>> GetUserSchedules(int PageNumber, int Count,Guid Id)=> await _db.Schedules.Include(x=>x.Subject).Where(x=>x.User.ID==Id).Skip((PageNumber - 1) * Count).Take(Count).ToListAsync();
        public async Task<IEnumerable<Schedule>> GetUserSchedules(Guid Id) => await _db.Schedules.Include(x => x.Subject).Where(x => x.User.ID == Id).ToListAsync();

    }
}
