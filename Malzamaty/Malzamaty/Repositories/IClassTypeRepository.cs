using AutoMapper;
using Malzamaty.Model;
using Malzamaty.Repositories;

namespace Malzamaty.Services
{
    public interface IClassTypeRepository : IBaseRepository<ClassType>
    {
    }
    public class ClassTypeRepository : BaseRepository<ClassType>, IClassTypeRepository
    {
        private readonly MalzamatyContext _db;
        protected readonly IMapper _mapper;

        public ClassTypeRepository(MalzamatyContext context, IMapper mapper) : base(context, mapper)
        {
            _db = context;
        }
    }
}
