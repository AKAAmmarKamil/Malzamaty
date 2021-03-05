using AutoMapper;
using Malzamaty.Model;
using Malzamaty.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Malzamaty
{
    public interface ISubjectRepository : IBaseRepository<Subject>
    {
        Task<IEnumerable<Subject>> GetAll();
    }
    public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
    {
        private readonly MalzamatyContext _db;
        public SubjectRepository(MalzamatyContext context, IMapper mapper) : base(context, mapper)
        {
            _db = context;
        }

        public async Task<IEnumerable<Subject>> GetAll() => await _db.Subject.ToListAsync();

    }
}
