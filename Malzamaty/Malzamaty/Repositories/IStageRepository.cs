using AutoMapper;
using Malzamaty.Model;
using Malzamaty.Repositories;

namespace Malzamaty.Services
{
    public interface IStageRepository : IBaseRepository<Stage>
    {
    }
    public class StageRepository : BaseRepository<Stage>, IStageRepository
    {
        private readonly MalzamatyContext _db;
        public StageRepository(MalzamatyContext context, IMapper mapper) : base(context, mapper)
        {
            _db = context;
        }
    }
}
