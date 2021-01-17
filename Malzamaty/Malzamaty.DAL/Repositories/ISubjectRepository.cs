using Malzamaty.Dto;
using Malzamaty.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Malzamaty.Services
{
    public interface ISubjectRepository : IBaseRepository<Subject>
    {
    }
    public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
    {
        private readonly MalzamatyContext _db;
        public SubjectRepository(MalzamatyContext context) : base(context)
        {
            _db = context;
        }

    }
}
