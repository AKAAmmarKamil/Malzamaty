using Malzamaty.Model;
using Malzamaty.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Services
{
    public interface IMatchRepository : IBaseRepository<Match>
    {
        Task<IEnumerable<Match>> FindByClass(Guid Id, Guid Subject);
    }
    public class MatchRepository : BaseRepository<Match>, IMatchRepository
    {
        private readonly MalzamatyContext _db;
        public MatchRepository(MalzamatyContext context) : base(context)
        {
            _db = context;
        }
        public async Task<Match> FindById(Guid id)
        {
            var Result = await _db.Matches.Include(x => x.Class).Include(x => x.Subject).FirstOrDefaultAsync(x => x.ID == id);
            if (Result == null) return null;
            return Result;
        }
        public async Task<IEnumerable<Match>> FindAll(int PageNumber, int count) =>
        await _db.Matches.Include(x => x.Class).Include(x => x.Subject)
                  .Skip((PageNumber - 1) * count).Take(count).ToListAsync();
        public async Task<IEnumerable<Match>> FindByClass(Guid Class,Guid Subject)=>
        await _db.Matches.Where(x=>x.ClassID==Class && x.SubjectID==Subject).Include(x => x.Class).Include(x => x.Subject).ToListAsync();
    }
}
