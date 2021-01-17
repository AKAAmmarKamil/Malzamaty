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
        public StageRepository(MalzamatyContext context) : base(context)
        {
            _db = context;
        }
    }
}
