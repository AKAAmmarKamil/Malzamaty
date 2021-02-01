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
    public interface IInterestRepository : IBaseRepository<Interests>
    {
        Task<List<Interests>> GetInterests(Guid Id);
        Task<bool> CheckIfLast(Guid id);
        Task<Interests> FindByUser(Guid Id);
    }
    public class InterestsRepository : BaseRepository<Interests>, IInterestRepository
    {
        private readonly MalzamatyContext _db;
        protected readonly IMapper _mapper;
        public InterestsRepository(MalzamatyContext context, IMapper mapper) : base(context, mapper)
        {
            _db = context;
        }
        public async Task<IEnumerable<Interests>> FindAll(int PageNumber, int count) => await _db.Interests.Include(x => x.User).Include(x => x.Class).Include(x => x.Subject).Skip((PageNumber - 1) * count).Take(count).ToListAsync();

        public async Task<List<Interests>> GetInterests(Guid Id) => await _db.Interests.Include(x => x.Subject).Include(x => x.Class).ThenInclude(x => x.Stage).Include(x => x.Class).ThenInclude(x => x.ClassType).Where(x => x.UserID == Id).ToListAsync();
        public async Task<Interests> FindById(Guid Id)
        {
            var Result = await _db.Interests.Include(x => x.User).Include(x => x.Class).Include(x => x.Subject).FirstOrDefaultAsync(x => x.ID == Id);

            if (Result == null) return null;
            return Result;
        }
        public async Task<Interests> FindByUser(Guid Id)
        {
            var Result = await _db.Interests.Where(x => x.UserID == Id).Include(x => x.User).Include(x => x.Class).Include(x => x.Subject).FirstOrDefaultAsync();

            if (Result == null) return null;
            return Result;
        }
        public async Task<bool> CheckIfLast(Guid id)
        {
            var Result = await _db.Interests.Include(x => x.User).Include(x => x.Class).Include(x => x.Subject).FirstOrDefaultAsync(x => x.ID == id);
            var CheckIfLast = _db.Interests.Where(x => x.User == Result.User).ToListAsync();
            if (CheckIfLast.Result.Count <= 1) return false;
            return true;
        }
    }
}