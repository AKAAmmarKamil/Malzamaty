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
    public class MatchRepository : BaseRepository<Match>, IMatchRepository
    {
        private readonly TheContext _db;
        public MatchRepository(TheContext context) : base(context)
        {
            _db = context;
        }
        public async Task<Match> FindById(Guid id)=>
             await _db.Matches.Include(x => x.Class).Include(x=>x.Subject).FirstOrDefaultAsync(x => x.ID == id);
        public async Task<List<Match>> GetAll(int PageNumber, int count)
        {
            var User = await _db.Matches.Include(x => x.Class).Include(x=>x.Subject)
                      .Skip((PageNumber - 1) * count).Take(count).ToListAsync();
            return User;
        }
    }
}
