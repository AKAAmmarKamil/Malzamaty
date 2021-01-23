using AutoMapper;
using Malzamaty.Model;
using Malzamaty.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty
{
    public interface IClassRepository : IBaseRepository<Class>
    {
    }
    public class ClassRepository : BaseRepository<Class>, IClassRepository
    {
        private readonly MalzamatyContext _db;
        protected readonly IMapper _mapper;

        public ClassRepository(MalzamatyContext context, IMapper mapper) : base(context, mapper)
        {
            _db = context;
        }
        public async Task<Class> FindById(Guid id)
        {
            var Result = await _db.Class.Include(x => x.Stage).Include(x => x.ClassType).Include(x => x.Country).FirstOrDefaultAsync(x => x.ID == id);
            if (Result == null) return null;
            return Result;
        }
        public async Task<IEnumerable<Class>> FindAll(int PageNumber, int count) => await _db.Class.Include(x => x.Stage).Include(x => x.ClassType).Include(x => x.Country).Skip((PageNumber - 1) * count).Take(count).ToListAsync();

    }
}
