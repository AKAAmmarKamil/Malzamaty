using Malzamaty.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Malzamaty.Services
{
    public interface IMatchRepository : IBaseRepository<Match>
    {
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
    }
}
