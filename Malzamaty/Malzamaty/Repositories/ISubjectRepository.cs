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
        protected readonly Mapper _mapper;
        public SubjectRepository(MalzamatyContext context, Mapper mapper) : base(context, mapper)
        {
            _db = context;
        }

    }
}
