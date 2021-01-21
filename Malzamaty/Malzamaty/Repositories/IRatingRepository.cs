using Malzamaty.Model;
using Malzamaty.Repositories;
namespace Malzamaty.Services
{
    public interface IRatingRepository : IBaseRepository<Rating>
    {
    }
    public class RatingRepository : BaseRepository<Rating>, IRatingRepository
    {
        private readonly MalzamatyContext _db;
        public RatingRepository(MalzamatyContext context) : base(context)
        {
            _db = context;
        }
        
    }
}
