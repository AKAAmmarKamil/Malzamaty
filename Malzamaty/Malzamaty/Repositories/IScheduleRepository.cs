using Malzamaty.Model;
using Malzamaty.Repositories;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Malzamaty.Services
{
    public interface IScheduleRepository : IBaseRepository<Schedule>
    {
        Task<IEnumerable<Schedule>> GetUserSchedules(int PageNumber, int Count, Guid Id);
    }
    public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
    {
        private readonly MalzamatyContext _db;
        public ScheduleRepository(MalzamatyContext context) : base(context)
        {
            _db = context;
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
        public async Task<IEnumerable<Schedule>> GetUserSchedules(int PageNumber, int Count,Guid Id)
        {
            return await _db.Schedules.Include(x=>x.Subject).Where(x=>x.User.ID==Id).Skip((PageNumber - 1) * Count).Take(Count).ToListAsync();
        }
    }
}
