using Malzamaty.Model;
using Malzamaty.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Services
{
    public interface IFileRepository : IBaseRepository<File>
    {
    }
    public class FileRepository : BaseRepository<File>, IFileRepository
    {
        private readonly MalzamatyContext _db;
        public FileRepository(MalzamatyContext context) : base(context)
        {
            _db = context;
        }
        public async Task<File> FindById(Guid id)
        {
            var Result = await _db.File.Include(x => x.User).Include(x => x.Class).Include(x => x.Subject).FirstOrDefaultAsync(x => x.ID == id);

            if (Result == null) return null;
            return Result;
        }    
        public async Task<IEnumerable<File>> FindAll(int PageNumber, int count) => await _db.File.Include(x => x.User).Include(x => x.Class).Include(x => x.Subject).Skip((PageNumber - 1) * count).Take(count).ToListAsync();

    }
}
