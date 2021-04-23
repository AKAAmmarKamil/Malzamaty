using AutoMapper;
using Malzamaty.Model;
using Malzamaty.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty
{
    public interface ILibraryRepository : IBaseRepository<Library>
    {
    }
    public class LibraryRepository : BaseRepository<Library>, ILibraryRepository
    {
        private readonly MalzamatyContext _db;
        public LibraryRepository(MalzamatyContext context, IMapper mapper) : base(context, mapper)
        {
            _db = context;
        }

        public async Task<IEnumerable<Library>> FindAll(int PageNumber, int Count) => await _db.Library.Include(x => x.Address).Skip((PageNumber - 1) * Count).Take(Count).ToListAsync();
    }
}
