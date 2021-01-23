using AutoMapper;
using Malzamaty.Model;
using Malzamaty.Repositories;
using System;

namespace Malzamaty.Services
{
    public interface IClassTypeRepository : IBaseRepository<ClassType>
    {
    }
    public class ClassTypeRepository : BaseRepository<ClassType>, IClassTypeRepository
    {
        private readonly MalzamatyContext _db;
        protected readonly Mapper _mapper;

        public ClassTypeRepository(MalzamatyContext context, Mapper mapper) : base(context, mapper)
        {
            _db = context;
        }
    }
}
