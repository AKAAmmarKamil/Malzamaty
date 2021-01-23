using AutoMapper;
using Malzamaty.Model;
using Malzamaty.Repositories;
using System;

namespace Malzamaty
{
    public interface ISubjectRepository : IBaseRepository<Subject>
    {
    }
    public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
    {
        private readonly MalzamatyContext _db;
        protected readonly IMapper _mapper;
        public SubjectRepository(MalzamatyContext context, IMapper mapper) : base(context, mapper)
        {
            _db = context;
        }

    }
}
