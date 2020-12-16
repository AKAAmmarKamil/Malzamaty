using Malzamaty.Dto;
using Malzamaty.Model;
using Malzamaty.Services;
using System;
using System.Threading.Tasks;

namespace Malzamaty.Repository
{
    public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
    {
        private readonly TheContext _db;
        public SubjectRepository(TheContext context) : base(context)
        {
            _db = context;
        }

    }
}
