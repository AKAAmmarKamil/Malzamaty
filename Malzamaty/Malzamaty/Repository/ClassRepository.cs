using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Malzamaty.Model;
using Malzamaty.Dto;
using Malzamaty.Services;
using System.Collections.Generic;
using System.Linq;

namespace Malzamaty.Repository
{
    public class ClassRepository : BaseRepository<Class>, IClassRepository
    {
        private readonly TheContext _db;
        public ClassRepository(TheContext context) : base(context)
        {
            _db = context;
        }
        public async Task<Class> FindById(Guid id)
        {
            var Result = _db.Class.Include(x => x.Stage).Include(x=>x.ClassType).Include(x => x.Country).FirstOrDefaultAsync(x=>x.ID==id);
            
            if (Result == null) return null;
            return await Result;
        }
        public async Task<IEnumerable<Class>> FindAll(int PageNumber, int count)
        {
            return await _db.Class.Include(x => x.Stage).Include(x => x.ClassType).Include(x => x.Country).Skip((PageNumber-1) * count).Take(count).ToListAsync();
        }
    }
}
